Imports ClassL.accesodatosSQL
Imports System.Net.Mail

Public Class registro
    Inherits System.Web.UI.Page

    Protected Sub registro_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String
        result = conectar()

    End Sub
    Protected Sub Enviar_Click(sender As Object, e As EventArgs) Handles Enviar.Click
        Dim result As String
        Dim NumConf As Integer
        'Dim mail As New MailMessage()
        'Dim enlace As String
        'Randomize()
        'NumConf = CLng(Rnd() * 9000000) + 1000000
        'enlace = "http://localhost:50139/confirmacion.aspx?"
        'enlace = enlace + "mbr=" + Correo.Text + "&numconf=" & NumConf
        'enlace = "http://hads21hads.azurewebsites.net/confirmacion.aspx?"
        'enlace = String.Concat("http://hads21hads.azurewebsites.net/confirmacion.aspx?mbr=", Correo.Text, "&numconf=", CStr(NumConf))


        Dim contraseñaCript As String

        contraseñaCript = getMd5Hash(Contraseña.Text)


        result = insertar(Correo.Text, Nombre.Text, Apellidos.Text, Pregunta.Text,
                          Respuesta.Text, DNI.Text, NumConf, 1, Nombre.Text, Nombre.Text, contraseñaCript)


        ' enviarEmail(Correo.Text, enlace)
        Response.Redirect("inicio.aspx")

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("inicio.aspx")
    End Sub

    Private Sub registro_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        cerrarconexion()
    End Sub

    Public Function enviarEmail(ByVal email As String, ByVal enlace As String) As Boolean
        Try
            'Direccion de origen
            Dim from_address As New MailAddress("hads21hads@gmail.com", "hads21")
            'Direccion de destino
            Dim to_address As New MailAddress(email)
            'Password de la cuenta
            Dim from_pass As String = "PerroPerro1"
            'Objeto para el cliente smtp
            Dim smtp As New SmtpClient
            'Host en este caso el servidor de gmail
            smtp.Host = "smtp.gmail.com"
            'Puerto
            smtp.Port = 587
            'SSL activado para que se manden correos de manera segura
            smtp.EnableSsl = True
            'No usar los credenciales por defecto ya que si no no funciona
            smtp.UseDefaultCredentials = False
            'Credenciales
            smtp.Credentials = New System.Net.NetworkCredential(from_address.Address, from_pass)
            'Creamos el mensaje con los parametros de origen y destino
            Dim message As New MailMessage(from_address, to_address)
            'Añadimos el asunto
            message.Subject = "confirmacion registro"
            'Concatenamos el cuerpo del mensaje a la plantilla
            message.Body = "<html><head></head><body>" + enlace + "</body></html>"
            'Definimos el cuerpo como html para poder escojer mejor como lo mandamos
            message.IsBodyHtml = True
            'Se envia el correo
            smtp.Send(message)
        Catch e As Exception
            Return False
        End Try
        Return True
    End Function

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