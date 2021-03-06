Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq

Namespace $namespace$

    Friend NotInheritable Class Configuration $contextTypeComment$
        Inherits DbMigrationsConfiguration(Of $contextType$)

        Public Sub New()
            AutomaticMigrationsEnabled = $enableAutomaticMigrations$$migrationsDirectory$$contextKey$
        End Sub

        Protected Overrides Sub Seed(context As $contextType$)
            '  This method will be called after migrating to the latest version.

            '  You can use the DbSet(Of T).AddOrUpdate() helper extension method 
            '  to avoid creating duplicate seed data.
        End Sub

    End Class

End Namespace
