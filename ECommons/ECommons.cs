﻿using Dalamud.Plugin;
using ECommons.DalamudServices;
using ECommons.GameFunctions;
using ECommons.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommons
{
    public static class ECommons
    {
        public static void Init(DalamudPluginInterface pluginInterface)
        {
            GenericHelpers.Safe(() => Svc.Init(pluginInterface));
            GenericHelpers.Safe(ObjectFunctions.Init);
            GenericHelpers.Safe(DalamudReflector.Init);
        }
    }
}