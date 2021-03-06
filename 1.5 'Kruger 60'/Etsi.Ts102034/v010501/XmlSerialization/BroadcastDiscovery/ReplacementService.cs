﻿// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    /// <summary>
    /// [en] implementation of The replacement_Service part of the Linkage_Descriptor in ETSI EN 300 468. Describes a service to try if the specified one fails.
    /// </summary>
    /// <remarks>Schema origin: urn:dvb:metadata:iptv:sdns:2012-1:ReplacementService</remarks>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class ReplacementService
    {
        [XmlElement("DVBTriplet", typeof(DvbTriplet))]
        public DvbTriplet DvbTriplet;

        [XmlElement("TextualIdentifier", typeof(TextualIdentifier))]
        public TextualIdentifier TextualIdentifier;

        [XmlAttribute("ReplacementType")]
        [DefaultValue("5")]
        public string Kind;

        public ReplacementService()
        {
            this.Kind = "5";
        } // default constructor
    } // class ReplacementService
} // namespace
