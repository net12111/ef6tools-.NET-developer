﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Database
{
    internal interface IDataSchemaServer
    {
        IDataSchemaProcedure GetProcedureOrFunction(string schemaName, string objectName);
    }
}
