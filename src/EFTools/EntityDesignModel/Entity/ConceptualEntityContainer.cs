// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.Design.Model.Entity
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Linq;

    internal class ConceptualEntityContainer : BaseEntityContainer
    {
        // FunctionImport elements only exist in the csdl file
        private readonly List<FunctionImport> _functionImports = new List<FunctionImport>();
        private DefaultableValue<string> _typeAccessAttr;
        private DefaultableValue<bool> _lazyLoadingEnabledAttr;

        internal ConceptualEntityContainer(EFElement parent, XElement element)
            : base(parent, element)
        {
        }

        /// <summary>
        ///     Manages the content of the TypeAccess attribute
        /// </summary>
        internal DefaultableValue<string> TypeAccess
        {
            get
            {
                if (_typeAccessAttr == null)
                {
                    _typeAccessAttr = new TypeAccessDefaultableValue(this);
                }
                return _typeAccessAttr;
            }
        }

        /// <summary>
        ///     Manages the content of the LazyLoadingEnabled attribute
        /// </summary>
        internal DefaultableValue<bool> LazyLoadingEnabled
        {
            get
            {
                if (_lazyLoadingEnabledAttr == null)
                {
                    _lazyLoadingEnabledAttr = new LazyLoadingDefaultableValue(this);
                }
                return _lazyLoadingEnabledAttr;
            }
        }

        internal void AddFunctionImport(FunctionImport fi)
        {
            _functionImports.Add(fi);
        }

        internal IList<FunctionImport> FunctionImports()
        {
            return _functionImports.AsReadOnly();
        }

        // we unfortunately get a warning from the compiler when we use the "base" keyword in "iterator" types generated by using the
        // "yield return" keyword.  By adding this method, I was able to get around this.  Unfortunately, I wasn't able to figure out
        // a way to implement this once and have derived classes share the implementation (since the "base" keyword is resolved at 
        // compile-time and not at runtime.
        private IEnumerable<EFObject> BaseChildren
        {
            get { return base.Children; }
        }

        internal override IEnumerable<EFObject> Children
        {
            get
            {
                foreach (var efobj in BaseChildren)
                {
                    yield return efobj;
                }

                foreach (var child in _functionImports)
                {
                    yield return child;
                }
                yield return TypeAccess;
                yield return LazyLoadingEnabled;
            }
        }

        protected override void OnChildDeleted(EFContainer efContainer)
        {
            var child = efContainer as FunctionImport;
            if (child != null)
            {
                _functionImports.Remove(child);
                return;
            }

            base.OnChildDeleted(efContainer);
        }

#if DEBUG
        internal override ICollection<string> MyChildElementNames()
        {
            var s = base.MyChildElementNames();
            s.Add(FunctionImport.ElementName);
            return s;
        }

        internal override ICollection<string> MyAttributeNames()
        {
            var s = base.MyAttributeNames();
            s.Add(TypeAccessDefaultableValue.AttributeTypeAccess);
            s.Add(LazyLoadingDefaultableValue.AttributeLazyLoadingEnabled);
            return s;
        }
#endif

        protected override void PreParse()
        {
            Debug.Assert(State != EFElementState.Parsed, "this object should not already be in the parsed state");

            ClearEntitySets();
            ClearEFObjectCollection(_functionImports);
            ClearEFObject(_typeAccessAttr);
            ClearEFObject(_lazyLoadingEnabledAttr);
            _lazyLoadingEnabledAttr = null;

            base.PreParse();
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal override bool ParseSingleElement(ICollection<XName> unprocessedElements, XElement elem)
        {
            if (elem.Name.LocalName == EntitySet.ElementName)
            {
                EntitySet es = new ConceptualEntitySet(this, elem);
                AddEntitySet(es);
                es.Parse(unprocessedElements);
            }
            else if (elem.Name.LocalName == FunctionImport.ElementName)
            {
                var fi = new FunctionImport(this, elem);
                _functionImports.Add(fi);
                fi.Parse(unprocessedElements);
            }
            else
            {
                return base.ParseSingleElement(unprocessedElements, elem);
            }
            return true;
        }

        protected override void DoNormalize()
        {
            NormalizedName = new Symbol(LocalName.Value);
            base.DoNormalize();
        }
    }
}
