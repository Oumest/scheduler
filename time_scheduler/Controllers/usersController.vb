Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports time_scheduler

Namespace Controllers
    Public Class usersController
        Inherits System.Web.Http.ApiController

        Private db As New schedulerEntities

        ' GET: api/users
        Function Getusers() As IQueryable(Of users)
            Return db.users
        End Function

        ' GET: api/users/5
        <ResponseType(GetType(users))>
        Function Getusers(ByVal id As Integer) As IHttpActionResult
            Dim users As users = db.users.Find(id)
            If IsNothing(users) Then
                Return NotFound()
            End If

            Return Ok(users)
        End Function

        ' PUT: api/users/5
        <ResponseType(GetType(Void))>
        Function Putusers(ByVal id As Integer, ByVal users As users) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = users.id Then
                Return BadRequest()
            End If

            db.Entry(users).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (usersExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/users
        <ResponseType(GetType(users))>
        Function Postusers(ByVal users As users) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.users.Add(users)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = users.id}, users)
        End Function

        ' DELETE: api/users/5
        <ResponseType(GetType(users))>
        Function Deleteusers(ByVal id As Integer) As IHttpActionResult
            Dim users As users = db.users.Find(id)
            If IsNothing(users) Then
                Return NotFound()
            End If

            db.users.Remove(users)
            db.SaveChanges()

            Return Ok(users)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function usersExists(ByVal id As Integer) As Boolean
            Return db.users.Count(Function(e) e.id = id) > 0
        End Function
    End Class
End Namespace