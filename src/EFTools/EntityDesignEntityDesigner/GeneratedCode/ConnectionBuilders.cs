
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Microsoft.Data.Entity.Design.EntityDesigner
{
	/// <summary>
	/// ConnectionBuilder class to provide logic for constructing connections between elements.
	/// </summary>
	internal static partial class AssociationBuilder
	{
		#region Accept Connection Methods
		/// <summary>
		/// Test whether a given model element is acceptable to this ConnectionBuilder as the source of a connection.
		/// </summary>
		/// <param name="candidate">The model element to test.</param>
		/// <returns>Whether the element can be used as the source of a connection.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static bool CanAcceptSource(DslModeling::ModelElement candidate)
		{
			if (candidate == null) return false;
			else if (candidate is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
			{ 
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Test whether a given model element is acceptable to this ConnectionBuilder as the target of a connection.
		/// </summary>
		/// <param name="candidate">The model element to test.</param>
		/// <returns>Whether the element can be used as the target of a connection.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static bool CanAcceptTarget(DslModeling::ModelElement candidate)
		{
			if (candidate == null) return false;
			else if (candidate is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
			{ 
				return true;
			}
			else
				return false;
		}
		
		/// <summary>
		/// Test whether a given pair of model elements are acceptable to this ConnectionBuilder as the source and target of a connection
		/// </summary>
		/// <param name="candidateSource">The model element to test as a source</param>
		/// <param name="candidateTarget">The model element to test as a target</param>
		/// <returns>Whether the elements can be used as the source and target of a connection</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Generated code.")]
		public static bool CanAcceptSourceAndTarget(DslModeling::ModelElement candidateSource, DslModeling::ModelElement candidateTarget)
		{
			// Accepts null, null; source, null; source, target but NOT null, target
			if (candidateSource == null)
			{
				if (candidateTarget != null)
				{
					throw new global::System.ArgumentNullException("candidateSource");
				}
				else // Both null
				{
					return false;
				}
			}
			bool acceptSource = CanAcceptSource(candidateSource);
			// If the source wasn't accepted then there's no point checking targets.
			// If there is no target then the source controls the accept.
			if (!acceptSource || candidateTarget == null)
			{
				return acceptSource;
			}
			else // Check combinations
			{
				if (candidateSource is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
				{
					if (candidateTarget is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
					{
						return true;
					}
				}
				
			}
			return false;
		}
		#endregion

		#region Connection Methods
		/// <summary>
		/// Make a connection between the given pair of source and target elements
		/// </summary>
		/// <param name="source">The model element to use as the source of the connection</param>
		/// <param name="target">The model element to use as the target of the connection</param>
		/// <returns>A link representing the created connection</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Generated code.")]
		public static DslModeling::ElementLink Connect(DslModeling::ModelElement source, DslModeling::ModelElement target)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new global::System.ArgumentNullException("target");
			}
			
			if (CanAcceptSourceAndTarget(source, target))
			{
				if (source is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
				{
					if (target is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
					{
						global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType sourceAccepted = (global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)source;
						global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType targetAccepted = (global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)target;
						DslModeling::ElementLink result = new global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.Association(sourceAccepted, targetAccepted);
						if (DslModeling::DomainClassInfo.HasNameProperty(result))
						{
							DslModeling::DomainClassInfo.SetUniqueName(result);
						}
						return result;
					}
				}
				
			}
			global::System.Diagnostics.Debug.Fail("Having agreed that the connection can be accepted we should never fail to make one.");
			throw new global::System.InvalidOperationException();
		}
		#endregion
 	}
	/// <summary>
	/// ConnectionBuilder class to provide logic for constructing connections between elements.
	/// </summary>
	internal static partial class InheritanceBuilder
	{
		#region Accept Connection Methods
		/// <summary>
		/// Test whether a given model element is acceptable to this ConnectionBuilder as the source of a connection.
		/// </summary>
		/// <param name="candidate">The model element to test.</param>
		/// <returns>Whether the element can be used as the source of a connection.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static bool CanAcceptSource(DslModeling::ModelElement candidate)
		{
			if (candidate == null) return false;
			else if (candidate is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
			{ 
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Test whether a given model element is acceptable to this ConnectionBuilder as the target of a connection.
		/// </summary>
		/// <param name="candidate">The model element to test.</param>
		/// <returns>Whether the element can be used as the target of a connection.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static bool CanAcceptTarget(DslModeling::ModelElement candidate)
		{
			if (candidate == null) return false;
			else if (candidate is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
			{ 
				return true;
			}
			else
				return false;
		}
		
		/// <summary>
		/// Test whether a given pair of model elements are acceptable to this ConnectionBuilder as the source and target of a connection
		/// </summary>
		/// <param name="candidateSource">The model element to test as a source</param>
		/// <param name="candidateTarget">The model element to test as a target</param>
		/// <returns>Whether the elements can be used as the source and target of a connection</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Generated code.")]
		public static bool CanAcceptSourceAndTarget(DslModeling::ModelElement candidateSource, DslModeling::ModelElement candidateTarget)
		{
			// Accepts null, null; source, null; source, target but NOT null, target
			if (candidateSource == null)
			{
				if (candidateTarget != null)
				{
					throw new global::System.ArgumentNullException("candidateSource");
				}
				else // Both null
				{
					return false;
				}
			}
			bool acceptSource = CanAcceptSource(candidateSource);
			// If the source wasn't accepted then there's no point checking targets.
			// If there is no target then the source controls the accept.
			if (!acceptSource || candidateTarget == null)
			{
				return acceptSource;
			}
			else // Check combinations
			{
				if (candidateSource is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
				{
					if (candidateTarget is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
					{
						global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType sourceEntityType = (global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)candidateSource;
						global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType targetEntityType = (global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)candidateTarget;
						if(targetEntityType == null || global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.Inheritance.GetLinkToBaseType(targetEntityType) != null) return false;
						if(targetEntityType == null || sourceEntityType == null || global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.Inheritance.GetLinks(sourceEntityType, targetEntityType).Count > 0) return false;
						return true;
					}
				}
				
			}
			return false;
		}
		#endregion

		#region Connection Methods
		/// <summary>
		/// Make a connection between the given pair of source and target elements
		/// </summary>
		/// <param name="source">The model element to use as the source of the connection</param>
		/// <param name="target">The model element to use as the target of the connection</param>
		/// <returns>A link representing the created connection</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Generated code.")]
		public static DslModeling::ElementLink Connect(DslModeling::ModelElement source, DslModeling::ModelElement target)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new global::System.ArgumentNullException("target");
			}
			
			if (CanAcceptSourceAndTarget(source, target))
			{
				if (source is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
				{
					if (target is global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)
					{
						global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType sourceAccepted = (global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)source;
						global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType targetAccepted = (global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.EntityType)target;
						DslModeling::ElementLink result = new global::Microsoft.Data.Entity.Design.EntityDesigner.ViewModel.Inheritance(sourceAccepted, targetAccepted);
						if (DslModeling::DomainClassInfo.HasNameProperty(result))
						{
							DslModeling::DomainClassInfo.SetUniqueName(result);
						}
						return result;
					}
				}
				
			}
			global::System.Diagnostics.Debug.Fail("Having agreed that the connection can be accepted we should never fail to make one.");
			throw new global::System.InvalidOperationException();
		}
		#endregion
 	}
 	
 	/// <summary>
	/// Handles interaction between the ConnectionBuilder and the corresponding ConnectionTool.
	/// </summary>
	internal partial class AssociationToolConnectAction : DslDiagrams::ConnectAction
	{
		private DslDiagrams::ConnectionType[] connectionTypes;
		private global::System.Windows.Forms.Cursor sourceCursor;
		private global::System.Windows.Forms.Cursor targetCursor;
		
		/// <summary>
		/// Constructs a new AssociationToolConnectAction for the given Diagram.
		/// </summary>
		public AssociationToolConnectAction(DslDiagrams::Diagram diagram): base(diagram, true) 
		{
			global::System.Resources.ResourceManager resourceManager = global::Microsoft.Data.Entity.Design.EntityDesigner.MicrosoftDataEntityDesignDomainModel.SingletonResourceManager;
			global::System.Globalization.CultureInfo resourceCulture = global::System.Globalization.CultureInfo.CurrentUICulture;

			byte[] sourceCursorBytes = (byte[])resourceManager.GetObject("AssociationToolSourceCursor", resourceCulture);
			using(global::System.IO.MemoryStream sourceCursorStream = new global::System.IO.MemoryStream(sourceCursorBytes))
			{ 
				this.sourceCursor = new global::System.Windows.Forms.Cursor(sourceCursorStream);
			}
			byte[] targetCursorBytes = (byte[])resourceManager.GetObject("AssociationToolTargetCursor", resourceCulture);
			using(global::System.IO.MemoryStream targetCursorStream = new global::System.IO.MemoryStream(targetCursorBytes))
			{ 
				this.targetCursor = new global::System.Windows.Forms.Cursor(targetCursorStream);
			}
		}
		
		/// <summary>
		/// Gets the cursor corresponding to the given mouse position.
		/// </summary>
		/// <remarks>
		/// Changes the cursor to Cursors.No before the first mouse click if the source shape is not valid.
		/// </remarks>
		public override global::System.Windows.Forms.Cursor GetCursor(global::System.Windows.Forms.Cursor currentCursor, DslDiagrams::DiagramClientView diagramClientView, DslDiagrams::PointD mousePosition)
		{
			if (this.MouseDownHitShape == null && currentCursor != global::System.Windows.Forms.Cursors.No)
			{
				DslDiagrams::DiagramHitTestInfo hitTestInfo = new DslDiagrams::DiagramHitTestInfo(diagramClientView);
				this.Diagram.DoHitTest(mousePosition, hitTestInfo);
				DslDiagrams::ShapeElement shape = hitTestInfo.HitDiagramItem.Shape;

				DslDiagrams::ConnectionType connectionType = GetConnectionTypes(shape, null)[0];
				string warningString = string.Empty;
				if (!connectionType.CanCreateConnection(shape, null, ref warningString))
				{
					return global::System.Windows.Forms.Cursors.No;
				}
			}
			return base.GetCursor(currentCursor, diagramClientView, mousePosition);
		}
		
		/// <summary>
		/// Associates custom source and target cursors with the connect action.
		/// </summary>
		protected override global::System.Windows.Forms.Cursor GetCursorFromCursorType(DslDiagrams::ConnectActionCursor connectActionCursor)
		{
			if(connectActionCursor == DslDiagrams::ConnectActionCursor.Searching)
			{
				return this.sourceCursor;
			}
			if(connectActionCursor == DslDiagrams::ConnectActionCursor.Allowed)
			{
				return this.targetCursor;
			}
			return base.GetCursorFromCursorType(connectActionCursor);
		}
		
		/// <summary>
		/// Disposes custom cursor resources.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if(disposing)
				{
					if(this.sourceCursor != null)
					{
						this.sourceCursor.Dispose();
						this.sourceCursor = null;
					}
					if(this.targetCursor != null)
					{
						this.targetCursor.Dispose();
						this.targetCursor = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		/// <summary>
		/// Returns the AssociationToolConnectionType associated with this action.
		/// </summary>
		protected override DslDiagrams::ConnectionType[] GetConnectionTypes(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement)
		{
			if(this.connectionTypes == null)
			{
				this.connectionTypes = new DslDiagrams::ConnectionType[] { new AssociationToolConnectionType() };
			}
			
			return this.connectionTypes;
		}
		
		private partial class AssociationToolConnectionTypeBase : DslDiagrams::ConnectionType
		{
			/// <summary>
			/// Constructs a new the AssociationToolConnectionType with the given ConnectionBuilder.
			/// </summary>
			protected AssociationToolConnectionTypeBase() : base() {}
			
			private static DslDiagrams::ShapeElement RemovePassThroughShapes(DslDiagrams::ShapeElement shape)
			{
				if (shape is DslDiagrams::Compartment)
				{
					return shape.ParentShape;
				}
				DslDiagrams::SwimlaneShape swimlane = shape as DslDiagrams::SwimlaneShape;
				if (swimlane != null && swimlane.ForwardDragDropToParent)
				{
					return shape.ParentShape;
				}
				return shape;
			}
						
			/// <summary>
			/// Called by the base ConnectAction class to determine if the given shapes can be connected.
			/// </summary>
			/// <remarks>
			/// This implementation delegates calls to the ConnectionBuilder AssociationBuilder.
			/// </remarks>
			public override bool CanCreateConnection(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement, ref string connectionWarning)
			{
				bool canConnect = true;
				
				if(sourceShapeElement == null) throw new global::System.ArgumentNullException("sourceShapeElement");
				sourceShapeElement = RemovePassThroughShapes(sourceShapeElement);
				DslModeling::ModelElement sourceElement = sourceShapeElement.ModelElement;
				if(sourceElement == null) sourceElement = sourceShapeElement;
				
				DslModeling::ModelElement targetElement = null;
				if (targetShapeElement != null)
				{
					targetShapeElement = RemovePassThroughShapes(targetShapeElement);
					targetElement = targetShapeElement.ModelElement;
					if(targetElement == null) targetElement = targetShapeElement;
			
				}

				// base.CanCreateConnection must be called to check whether existing Locks prevent this link from getting created.	
				canConnect = base.CanCreateConnection(sourceShapeElement, targetShapeElement, ref connectionWarning);
				if (canConnect)
				{				
					if(targetShapeElement == null)
					{
						return AssociationBuilder.CanAcceptSource(sourceElement);
					}
					else
					{				
						return AssociationBuilder.CanAcceptSourceAndTarget(sourceElement, targetElement);
					}
				}
				else
				{
					//return false
					return canConnect;
				}
			}
						
			/// <summary>
			/// Called by the base ConnectAction class to ask whether the given source and target are valid.
			/// </summary>
			/// <remarks>
			/// Always return true here, to give CanCreateConnection a chance to decide.
			/// </remarks>
			public override bool IsValidSourceAndTarget(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement)
			{
				return true;
			}
			
			/// <summary>
			/// Called by the base ConnectAction class to create the underlying relationship.
			/// </summary>
			/// <remarks>
			/// This implementation delegates calls to the ConnectionBuilder AssociationBuilder.
			/// </remarks>
			public override void CreateConnection(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement, DslDiagrams::PaintFeedbackArgs paintFeedbackArgs)
			{
				if(sourceShapeElement == null) throw new global::System.ArgumentNullException("sourceShapeElement");
				if(targetShapeElement == null) throw new global::System.ArgumentNullException("targetShapeElement");
				
				sourceShapeElement = RemovePassThroughShapes(sourceShapeElement);
				targetShapeElement = RemovePassThroughShapes(targetShapeElement);
				
				DslModeling::ModelElement sourceElement = sourceShapeElement.ModelElement;
				if(sourceElement == null) sourceElement = sourceShapeElement;
				DslModeling::ModelElement targetElement = targetShapeElement.ModelElement;
				if(targetElement == null) targetElement = targetShapeElement;
				AssociationBuilder.Connect(sourceElement, targetElement);
			}
		}
		
		private partial class AssociationToolConnectionType : AssociationToolConnectionTypeBase
		{
			/// <summary>
			/// Constructs a new the AssociationToolConnectionType with the given ConnectionBuilder.
			/// </summary>
			public AssociationToolConnectionType() : base() {}
		}
	}
 	
 	/// <summary>
	/// Handles interaction between the ConnectionBuilder and the corresponding ConnectionTool.
	/// </summary>
	internal partial class InheritanceToolConnectAction : DslDiagrams::ConnectAction
	{
		private DslDiagrams::ConnectionType[] connectionTypes;
		private global::System.Windows.Forms.Cursor sourceCursor;
		private global::System.Windows.Forms.Cursor targetCursor;
		
		/// <summary>
		/// Constructs a new InheritanceToolConnectAction for the given Diagram.
		/// </summary>
		public InheritanceToolConnectAction(DslDiagrams::Diagram diagram): base(diagram, true) 
		{
			global::System.Resources.ResourceManager resourceManager = global::Microsoft.Data.Entity.Design.EntityDesigner.MicrosoftDataEntityDesignDomainModel.SingletonResourceManager;
			global::System.Globalization.CultureInfo resourceCulture = global::System.Globalization.CultureInfo.CurrentUICulture;

			byte[] sourceCursorBytes = (byte[])resourceManager.GetObject("InheritanceToolSourceCursor", resourceCulture);
			using(global::System.IO.MemoryStream sourceCursorStream = new global::System.IO.MemoryStream(sourceCursorBytes))
			{ 
				this.sourceCursor = new global::System.Windows.Forms.Cursor(sourceCursorStream);
			}
			byte[] targetCursorBytes = (byte[])resourceManager.GetObject("InheritanceToolTargetCursor", resourceCulture);
			using(global::System.IO.MemoryStream targetCursorStream = new global::System.IO.MemoryStream(targetCursorBytes))
			{ 
				this.targetCursor = new global::System.Windows.Forms.Cursor(targetCursorStream);
			}
		}
		
		/// <summary>
		/// Gets the cursor corresponding to the given mouse position.
		/// </summary>
		/// <remarks>
		/// Changes the cursor to Cursors.No before the first mouse click if the source shape is not valid.
		/// </remarks>
		public override global::System.Windows.Forms.Cursor GetCursor(global::System.Windows.Forms.Cursor currentCursor, DslDiagrams::DiagramClientView diagramClientView, DslDiagrams::PointD mousePosition)
		{
			if (this.MouseDownHitShape == null && currentCursor != global::System.Windows.Forms.Cursors.No)
			{
				DslDiagrams::DiagramHitTestInfo hitTestInfo = new DslDiagrams::DiagramHitTestInfo(diagramClientView);
				this.Diagram.DoHitTest(mousePosition, hitTestInfo);
				DslDiagrams::ShapeElement shape = hitTestInfo.HitDiagramItem.Shape;

				DslDiagrams::ConnectionType connectionType = GetConnectionTypes(shape, null)[0];
				string warningString = string.Empty;
				if (!connectionType.CanCreateConnection(shape, null, ref warningString))
				{
					return global::System.Windows.Forms.Cursors.No;
				}
			}
			return base.GetCursor(currentCursor, diagramClientView, mousePosition);
		}
		
		/// <summary>
		/// Associates custom source and target cursors with the connect action.
		/// </summary>
		protected override global::System.Windows.Forms.Cursor GetCursorFromCursorType(DslDiagrams::ConnectActionCursor connectActionCursor)
		{
			if(connectActionCursor == DslDiagrams::ConnectActionCursor.Searching)
			{
				return this.sourceCursor;
			}
			if(connectActionCursor == DslDiagrams::ConnectActionCursor.Allowed)
			{
				return this.targetCursor;
			}
			return base.GetCursorFromCursorType(connectActionCursor);
		}
		
		/// <summary>
		/// Disposes custom cursor resources.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if(disposing)
				{
					if(this.sourceCursor != null)
					{
						this.sourceCursor.Dispose();
						this.sourceCursor = null;
					}
					if(this.targetCursor != null)
					{
						this.targetCursor.Dispose();
						this.targetCursor = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		/// <summary>
		/// Returns the InheritanceToolConnectionType associated with this action.
		/// </summary>
		protected override DslDiagrams::ConnectionType[] GetConnectionTypes(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement)
		{
			if(this.connectionTypes == null)
			{
				this.connectionTypes = new DslDiagrams::ConnectionType[] { new InheritanceToolConnectionType() };
			}
			
			return this.connectionTypes;
		}
		
		private partial class InheritanceToolConnectionTypeBase : DslDiagrams::ConnectionType
		{
			/// <summary>
			/// Constructs a new the InheritanceToolConnectionType with the given ConnectionBuilder.
			/// </summary>
			protected InheritanceToolConnectionTypeBase() : base() {}
			
			private static DslDiagrams::ShapeElement RemovePassThroughShapes(DslDiagrams::ShapeElement shape)
			{
				if (shape is DslDiagrams::Compartment)
				{
					return shape.ParentShape;
				}
				DslDiagrams::SwimlaneShape swimlane = shape as DslDiagrams::SwimlaneShape;
				if (swimlane != null && swimlane.ForwardDragDropToParent)
				{
					return shape.ParentShape;
				}
				return shape;
			}
						
			/// <summary>
			/// Called by the base ConnectAction class to determine if the given shapes can be connected.
			/// </summary>
			/// <remarks>
			/// This implementation delegates calls to the ConnectionBuilder InheritanceBuilder.
			/// </remarks>
			public override bool CanCreateConnection(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement, ref string connectionWarning)
			{
				bool canConnect = true;
				
				if(sourceShapeElement == null) throw new global::System.ArgumentNullException("sourceShapeElement");
				sourceShapeElement = RemovePassThroughShapes(sourceShapeElement);
				DslModeling::ModelElement sourceElement = sourceShapeElement.ModelElement;
				if(sourceElement == null) sourceElement = sourceShapeElement;
				
				DslModeling::ModelElement targetElement = null;
				if (targetShapeElement != null)
				{
					targetShapeElement = RemovePassThroughShapes(targetShapeElement);
					targetElement = targetShapeElement.ModelElement;
					if(targetElement == null) targetElement = targetShapeElement;
					
					// The InheritanceTool connection tool specifies that source and target should be reversed.
					// base.CanCreateConnection must be called to check whether existing Locks prevent this link from getting created.
					canConnect = base.CanCreateConnection(targetShapeElement, sourceShapeElement, ref connectionWarning);
			
				}

				if (canConnect)
				{				
					if(targetShapeElement == null)
					{
						// The InheritanceTool connection tool specifies that source and target should be reversed. 
						return InheritanceBuilder.CanAcceptTarget(sourceElement);
					}
					else
					{				
						// The InheritanceTool connection tool specifies that source and target should be reversed. 
						return InheritanceBuilder.CanAcceptSourceAndTarget(targetElement, sourceElement);
					}
				}
				else
				{
					//return false
					return canConnect;
				}
			}
						
			/// <summary>
			/// Called by the base ConnectAction class to ask whether the given source and target are valid.
			/// </summary>
			/// <remarks>
			/// Always return true here, to give CanCreateConnection a chance to decide.
			/// </remarks>
			public override bool IsValidSourceAndTarget(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement)
			{
				return true;
			}
			
			/// <summary>
			/// Called by the base ConnectAction class to create the underlying relationship.
			/// </summary>
			/// <remarks>
			/// This implementation delegates calls to the ConnectionBuilder InheritanceBuilder.
			/// </remarks>
			public override void CreateConnection(DslDiagrams::ShapeElement sourceShapeElement, DslDiagrams::ShapeElement targetShapeElement, DslDiagrams::PaintFeedbackArgs paintFeedbackArgs)
			{
				if(sourceShapeElement == null) throw new global::System.ArgumentNullException("sourceShapeElement");
				if(targetShapeElement == null) throw new global::System.ArgumentNullException("targetShapeElement");
				
				sourceShapeElement = RemovePassThroughShapes(sourceShapeElement);
				targetShapeElement = RemovePassThroughShapes(targetShapeElement);
				
				DslModeling::ModelElement sourceElement = sourceShapeElement.ModelElement;
				if(sourceElement == null) sourceElement = sourceShapeElement;
				DslModeling::ModelElement targetElement = targetShapeElement.ModelElement;
				if(targetElement == null) targetElement = targetShapeElement;
				// The InheritanceTool connection tool specifies that source and target should be reversed. 
				InheritanceBuilder.Connect(targetElement, sourceElement);
			}
		}
		
		private partial class InheritanceToolConnectionType : InheritanceToolConnectionTypeBase
		{
			/// <summary>
			/// Constructs a new the InheritanceToolConnectionType with the given ConnectionBuilder.
			/// </summary>
			public InheritanceToolConnectionType() : base() {}
		}
	}
}

