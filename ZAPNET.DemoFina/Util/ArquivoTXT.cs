
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace ZAPNET.DemoFina.Util
{
    public class ArquivoTXT : Arquivo<List<string[]>>
    {
        public override async Task<List<string[]>> ReadFile(List<IFormFile> arquivos, IHostingEnvironment appEnvironment)
        {
            try
            {
                PathFile = await UploadFiles(arquivos, appEnvironment);

                using (StreamReader sr = File.OpenText(PathFile))
                {
                    List<string[]> lines = new List<string[]>();
                    List<string[]> validade = new List<string[]>();

                    int numeroLinha = 1;

                    while (!sr.EndOfStream)
                    {
                        string linha = await sr.ReadLineAsync();
                        string[] line;

                        /*HEADER FILE*/
                        if (linha.StartsWith("#A1"))
                        {
                            var tipoReg = linha.Substring(0, 3);
                            var codigoDocumento = linha.Substring(3, 4);
                            var cnpjOrIdBacen = linha.Substring(7, 8);
                            var filler = linha.Substring(15, 14);
                            var dataBase = linha.Substring(29, 6);
                            var tipoRemessa = linha.Substring(35, 1);
                            var filler2 = linha.Substring(36, 35);

                            line = new string[8];
                            line[0] = numeroLinha.ToString();
                            line[1] = tipoReg;
                            line[2] = codigoDocumento;
                            line[3] = cnpjOrIdBacen;
                            line[4] = filler;
                            line[5] = dataBase;
                            line[6] = tipoRemessa;
                            line[7] = filler2;

                            validade = validaEstruturaCadoc(line, "#A1");
                            lines.Add(line);
                        }
                        else if (linha.StartsWith("0"))
                        {
                            var tipoReg = linha.Substring(0, 1);
                            var codigoConta = linha.Substring(0, 10);
                            var filler = linha.Substring(10, 4);
                            var valorColuna1 = linha.Substring(14, 18);
                            var sinalColuna1 = linha.Substring(32, 1);
                            var valorColuna2 = linha.Substring(33, 18);
                            var sinalColuna2 = linha.Substring(51, 1);
                            var valorColuna3 = linha.Substring(52, 18);
                            var sinalColuna3 = linha.Substring(70, 1);

                            line = new string[10];
                            line[0] = numeroLinha.ToString();
                            line[1] = tipoReg;
                            line[2] = codigoConta;
                            line[3] = filler;
                            line[4] = valorColuna1;
                            line[5] = sinalColuna1;
                            line[6] = valorColuna2;
                            line[7] = sinalColuna2;
                            line[8] = valorColuna3;
                            line[9] = sinalColuna3;

                            validade = validaEstruturaCadoc(line, "0");
                            lines.Add(line);
                        }
                        else if (linha.StartsWith("@1"))
                        {
                            var tipoReg = linha.Substring(0, 2);
                            var numeroRegistros = linha.Substring(2, 6);
                            var filler = linha.Substring(8, 63);

                            line = new string[4];
                            line[0] = numeroLinha.ToString();
                            line[1] = tipoReg;
                            line[2] = numeroRegistros;
                            line[3] = filler;

                            validade = validaEstruturaCadoc(line, "@1");
                            lines.Add(line);
                        }
                        else
                        {
                            string[] valida = new string[4];
                            valida[0] = numeroLinha.ToString();
                            valida[1] = "ERROR";
                            valida[2] = "Campo 1";
                            valida[3] = "Inicialização da linha inválida (#A1 ou 0 ou @1); ";

                            validade.Add(valida);
                        }
                        //line = linha.Split(';');
                        numeroLinha++;
                    }

                    foreach (var item in validade)
                    {
                        if (item[1] == "ERROR")
                        {
                            return validade;
                        }
                    }

                    return lines;
                }
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }

        }


        private List<string[]> validaEstruturaCadoc(string[] linha, string inicializador)
        {
            List<string[]> lista = new List<string[]>();
            string[] validade = new string[4] { linha[0], "OK", "", "" };
            validade[0] = linha[0];


            //VALIDANDO REGISTRO DE IDENTIFICAÇÃO
            if (inicializador == "#A1")
            {

                /*
                //Solução mais eficiente em construção
                for (int i = 1; i <= 7; i++)
                {
                    var campo = linha[i];
                    var qtdErro = 0;

                    switch (int.Parse(campo))
                    {
                        case 1: // VALIDANDO CAMPO 2 - CÓDIGO DO DODUMENTO                   
                            if (!(campo == "4010" || campo == "4016" || campo == "4020" || campo == "4026"))
                            {
                                lista.Add(getError(linha[0], inicializador, (i + 1).ToString()));
                                qtdErro++;
                            }
                            break;

                        case 2: //VALIDANDO CAMPO 3 - CNPJ OU ID BACEN
                            if (!ehNumero(campo, "8"))
                            {
                                lista.Add(getError(linha[0], inicializador, (i + 1).ToString()));
                                qtdErro++;
                            }
                            break;

                        case 3: //VALIDANDO CAMPO 4 - FILLER
                            if (campo.Trim() != "")
                            {
                                lista.Add(getError(linha[0], inicializador, (i + 1).ToString()));
                                qtdErro++;
                            }
                            break;

                        case 4: //VALIDANDO CAMPO 5 - Data-base do documento.

                            var mes = int.Parse(campo.Substring(0, 2));
                            IEnumerable<int> meses = Enumerable.Range(1, 12);
                            var ano = int.Parse(campo.Substring(2, 4));
                            IEnumerable<int> anos = Enumerable.Range(2000, 2050);

                            if (!(meses.Contains(mes) || anos.Contains(ano)))
                            {
                                lista.Add(getError(linha[0], inicializador, (i + 1).ToString()));
                                qtdErro++;
                            }
                            break;

                        case 5: // VALIDANDO CAMPO 6 - TIPO DE REMESSA
                            if (!(campo == "I" || campo == "S"))
                            {
                                lista.Add(getError(linha[0], inicializador, (i + 1).ToString()));
                                qtdErro++;
                            }
                            break;

                        case 6: // VALIDANDO CAMPO 7 - FILLER
                            if (!(campo.Trim() == ""))
                            {
                                lista.Add(getError(linha[0], inicializador, (i + 1).ToString()));
                                qtdErro++;
                            }
                            break;

                        case 7: // SE NÃO HOUVER ERRO NA LINHA - REGITRAR ESTADO - OK
                            if(qtdErro == 0)
                            {
                                lista.Add(validade);
                            }
                            break;
                    }
                } */


                //VALIDANDO CAMPO 2 - CÓDIGO DO DODUMENTO
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = (linha[2] == "4010" || linha[2] == "4016" || linha[2] == "4020" || linha[2] == "4026") ? "OK" : "ERROR";
                validade[2] = "Campo 2";
                validade[3] = (validade[1] == "ERROR" ? "(004-007) Código do documento inválido (4010, 4016, 4020 ou 4026); " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 3 - CNPJ OU ID BACEN
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = Facilities.ehNumero(linha[3], "8") ? "OK" : "ERROR";
                validade[2] = "Campo 3";
                validade[3] = (validade[1] == "ERROR" ? "(008–015) Cnpj ou Id Bacen inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 4 - FILLER
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = linha[4].Trim() == "" ? "OK" : "ERROR";
                validade[2] = "Campo 4";
                validade[3] = (validade[1] == "ERROR" ? "(016-029) Filler deve ser branco; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 5 - Data-base do documento.
                validade = new string[4];
                validade[0] = linha[0];
                var mes = int.Parse(linha[5].Substring(0, 2));
                IEnumerable<int> meses = Enumerable.Range(1, 12);
                var ano = int.Parse(linha[5].Substring(2, 4));
                IEnumerable<int> anos = Enumerable.Range(2000, 2050);

                validade[1] = meses.Contains(mes) || anos.Contains(ano) ? "OK" : "ERROR";
                validade[2] = "Campo 5";
                validade[3] = (validade[1] == "ERROR" ? "(030-035) Data-base inválida; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 6 - TIPO DE REMESSA
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = linha[6] == "I" || linha[6] == "S" ? "OK" : "ERROR";
                validade[2] = "Campo 6";
                validade[3] = (validade[1] == "ERROR" ? "(036-036) Tipo da remessa inválida; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 7 - FILLER
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = linha[7].Trim() == "" ? "OK" : "ERROR";
                validade[2] = "Campo 7";
                validade[3] = (validade[1] == "ERROR" ? "(037-071) Filler deve ser branco; " : "");
                lista.Add(validade);

            }
            // VALIDANDO REGISTRO DE DADOS
            else if (inicializador == "0")
            {
                //VALIDANDO CAMPO 2 - CÓDIGO DE CONTA
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = Facilities.ehNumero(linha[2], "10") ? "OK" : "ERROR";
                validade[2] = "Campo 2";
                validade[3] = (validade[1] == "ERROR" ? "(001-010) Código de conta inválida; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 3 - FILLER
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = linha[3].Trim() == "" ? "OK" : "ERROR";
                validade[2] = "Campo 3";
                validade[3] = (validade[1] == "ERROR" ? "(011-014) Filler deve ser branco; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 4 - VALOR COLUNA 1
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = Facilities.ehNumero(linha[4], "18") ? "OK" : "ERROR";
                validade[2] = "Campo 4";
                validade[3] = (validade[1] == "ERROR" ? "(015–032) Valor inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 5 - SINAL COLUNA 1
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = (linha[5] == "+" || linha[5] == "-" || linha[5] == " ") ? "OK" : "ERROR";
                validade[2] = "Campo 5";
                validade[3] = (validade[1] == "ERROR" ? "(033-033) Sinal inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 6 - VALOR COLUNA 2
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = Facilities.ehNumero(linha[6], "18") ? "OK" : "ERROR";
                validade[2] = "Campo 4";
                validade[3] = (validade[1] == "ERROR" ? "(034–051) Valor inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 7 - SINAL COLUNA 2
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = (linha[7] == "+" || linha[7] == "-" || linha[7] == " ") ? "OK" : "ERROR";
                validade[2] = "Campo 5";
                validade[3] = (validade[1] == "ERROR" ? "(052-052) Sinal inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 8 - VALOR COLUNA 2 
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = Facilities.ehNumero(linha[8], "18") ? "OK" : "ERROR";
                validade[2] = "Campo 4";
                validade[3] = (validade[1] == "ERROR" ? "(053–070) Valor inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 9 - SINAL COLUNA 2
                validade = new string[4];
                validade[0] = linha[0];
                validade[1] = (linha[9] == "+" || linha[9] == "-" || linha[9] == " ") ? "OK" : "ERROR";
                validade[2] = "Campo 5";
                validade[3] = (validade[1] == "ERROR" ? "(071-071) Sinal inválido; " : "");
                lista.Add(validade);

            }
            // VALIDANDO REGISTRO DE CONTROLE FINAL
            else if (inicializador == "@1")
            {
                //VALIDANDO CAMPO 2 - NÚMERO DE REGISTROS
                validade[1] = int.Parse(linha[2]) == int.Parse(validade[0]) ? "OK" : "ERROR";
                validade[2] = "Campo 2";
                validade[3] = (validade[1] == "ERROR" ? "(003–008) Quantidade de linha(s) e de registro(s) informado(s) inválido; " : "");
                lista.Add(validade);

                //VALIDANDO CAMPO 3 - FILLER
                validade[1] = linha[3].Trim() == "" ? "OK" : "ERROR";
                validade[2] = "Campo 3";
                validade[3] = (validade[1] == "ERROR" ? "(009–071) Filler deve ser branco; " : "");
                lista.Add(validade);
            }

            return lista;

        }


        // METODO EM CONSTRUÇÃO - FALTA TERMINAR
        private string[] getError(string linha, string tipoReg, string campo)
        {
            string[] error = new string[4];
            Dictionary<string, string> errors = new Dictionary<string, string>();

            //ERROS DO REGISTRO DE IDENTIFICAÇÃO
            errors.Add("#A1-2", "(004-007) Código do documento inválido (4010, 4016, 4020 ou 4026); ");
            errors.Add("#A1-3", "(008–015) Cnpj ou Id Bacen inválido; ");
            errors.Add("#A1-4", "(016-029) Filler deve ser branco; ");
            errors.Add("#A1-5", "(030-035) Data-base inválida; ");
            errors.Add("#A1-6", "(036-036) Tipo da remessa inválida; ");

            //ERROS DO REGISTRO DE DADOS


            //ERROS DO REGISTRO DE CONTROLE FINAL


            // MANIPULANDO OS CAMPOS
            if (tipoReg == "#A1")
            {
                for (int i = 1; i < 4; i++)
                {
                    error[i - 1] = i == 0 ? linha : "";
                    error[i - 1] = i == 1 ? tipoReg : "";
                    error[i - 1] = i == 2 ? "Campo " + campo : "";
                    string erro;
                    errors.TryGetValue($"#A-{campo}", out erro);
                    error[i - 1] = i == 3 ? erro : "";
                }
            }

            return error;
        }
    }
}
