﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Util
{
    interface IArquivo<T>
    {
        T ReadFile();

    }
}
