namespace API.Models
{
    public class Reply
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string trace { get; set; }
    }
    public class ReplyLogin
    {
        public int Status { get; set; } //Tipo de estado para la respuesta: 200=OK, 400=BadRequest, 404=NotFound, 500=InternalServerError
        public bool Flag { get; set; } //Bandera que define si la validación fue exitosa o no.
        public string Message { get; set; } //Mensaje de error o exito
        public object Data { get; set; } //Cualquier tipo de información
    }

    public class ReplySucess
    {
        public bool Ok { get; set; } //Bandera que define si la validación fue exitosa o no.
        public string Message { get; set; } //Mensaje de error o exito
        public object Data { get; set; } //Cualquier tipo de información
    }


}
