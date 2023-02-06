namespace surfis_backend.Utils.Validators
{
    public static class PersonValidator
    {
        public static bool ValidateNameSurname(this string nameSurname)
        {
            // Иван Иванов
            // Михаил Салтыков-Щедрин
            // Мао Цзэдун

            // Имя должно быть единым, возможна двойная и тройная фамилия.

            try
            {
                if (string.IsNullOrWhiteSpace(nameSurname) || string.IsNullOrEmpty(nameSurname))
                    return false;

                var splitName = nameSurname.Split(" ");

                if (splitName.Length != 2)
                    return false;

                if (!char.IsUpper(splitName[0][0]) || !char.IsUpper(splitName[1][0]))
                    return false;

                if (splitName[0].Length <= 1 || splitName[1].Length <= 1)
                    return false;

                if (splitName[1].Split("").Count(m => m == "-") < 1) return true;
                var splitMultiplyName = splitName[1].Split('-');

                return splitMultiplyName.All(c => char.IsUpper(c[0]));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}