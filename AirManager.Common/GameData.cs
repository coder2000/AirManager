// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition;
using AirManager.Infrastructure.Models;

namespace AirManager.Infrastructure
{
    [Export(typeof (GameData))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GameData
    {
        public Country[] Countries { get; set; }

        public Airport[] Airports { get; set; }
    }
}