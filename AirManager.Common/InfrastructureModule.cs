﻿// Copyright 2014 Dieter Lunn All Rights Reserved

using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace AirManager.Infrastructure
{
    [ModuleExport(typeof (InfrastructureModule))]
    public class InfrastructureModule : IModule
    {
        public void Initialize()
        {
        }
    }
}