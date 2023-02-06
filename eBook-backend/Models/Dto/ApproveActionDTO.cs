namespace eBook_backend.Models.Dto
{
    // Single OK result - возврат одного значения. ( для POST запросов, например Guid )
    // Single BAD result - возврат состояние ошибки ERROR DTO

    /// <summary>
    /// Абстрактный класс для вывода результатов работы сервиса
    /// </summary>
    public abstract class ActionResultDto
    {
        public string Remarks { get; set; }

        /// <summary>.ctor</summary>
        public ActionResultDto(string remarks)
        {
            Remarks = remarks;
        }

        /// <summary>
        /// Являет ли результат положительным?
        /// </summary>
        public abstract bool IsOk();
    }

    /// <summary>
    /// Вывод положительного результата работы метода
    /// </summary>
    public class OkResultDto<T> : ActionResultDto where T : new()
    {
        /// <summary>
        /// Данные возвращаемого объекта
        /// </summary>
        public T Data { get; set; }

        public OkResultDto(T resultedObject, string remarks = "") : base(remarks)
        {
            Data = resultedObject;
        }

        /// <inheritdoc />
        public override bool IsOk() => true;
    }

    /// <summary>
    /// Возврат ошибки метода
    /// </summary>
    public class BadResultDto : ActionResultDto
    {
        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<ErrorDto> Errors { get; set; }

        /// <summary>.ctor</summary>
        public BadResultDto(List<ErrorDto> errors, string remarks = "") 
            : base(remarks) => this.Errors = errors;

        /// <summary>.ctor</summary>
        public BadResultDto(ErrorDto error, string remarks = "") 
            : base(remarks) => this.Errors = new List<ErrorDto> { error };

        /// <summary>.ctor</summary>
        public BadResultDto(string errorName, string errorMessage)
            : base("") => this.Errors = new List<ErrorDto> { new(errorName, errorMessage) };

        /// <summary>.ctor</summary>
        public BadResultDto(string errorMessage)
            : base("") => this.Errors = new List<ErrorDto> { new(errorMessage) };


        /// <inheritdoc />
        public override bool IsOk() => false;
    }

    /// <summary>
    /// Моедль ошибки проверки, в сервисах и контроллерах, используется в связке с ActionResultDto
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Название поля
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Ошибка, связанные с полем
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>.ctor</summary>
        public ErrorDto(string name, string errors)
        {
            this.Field = name;
            this.Messages = new List<string> { errors };
        }

        /// <summary>.ctor</summary>
        public ErrorDto(string errors)
        {
            this.Field = "__global";
            this.Messages = new List<string> { errors };
        }

        /// <summary>.ctor</summary>
        public ErrorDto(List<string> errors)
        {
            this.Field = "__global";
            this.Messages = errors;
        }

        /// <summary>.ctor</summary>
        public ErrorDto(string name, List<string> errors)
        {
            this.Field = name;
            this.Messages = errors;
        }
    }
}
