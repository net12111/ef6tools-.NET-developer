﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Entity
{
    using System.Xml.Linq;

    internal class Summary : TextNode
    {
        internal static readonly string ElementName = "Summary";

        internal Summary(EFContainer parent, XElement element)
            : base(parent, element)
        {
        }
    }
}
