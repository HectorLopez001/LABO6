Imports ClassL.accesodatosSQL
Public Class incio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim result As String
        result = conectar()

    End Sub


    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        cerrarconexion()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim result As String
        Dim tipo As String
        Dim contraseñaMd5 As String
        contraseñaMd5 = getMd5Hash(pass.Text)

        result = comprobarUsuario(usuario.Text, contraseñaMd5)
        tipo = tipoUsuario(usuario.Text)

        If result And tipo = "P" Then

            If usuario.Text = "vadillo@ehu.es" Then
                System.Web.Security.FormsAuthentication.SetAuthCookie("vadillo", False)
            Else
                System.Web.Security.FormsAuthentication.SetAuthCookie("profesor", False)
            End If
            Session("UserID") = usuario.Text()
            Response.Redirect("Profesor.aspx")

        ElseIf result And tipo = "A" Then
            System.Web.Security.FormsAuthentication.SetAuthCookie("alumno", False)

            Session("UserID") = usuario.Text()
            Response.Redirect("Alumno.aspx")
        Else
            Response.Write("<script>alert('Error... Correo y/o contraseña no validos')</script>")
        End If

    End Sub



    'Hash an input string and return the hash as a 32 character hexadecimal string.
    Function getMd5Hash(ByVal input As String) As String
        ' Create a new instance of the MD5CryptoServiceProvider object.
        Dim md5Hasher As New System.Security.Cryptography.MD5CryptoServiceProvider()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()
    End Function

End Class