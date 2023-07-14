using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ParanaBancoClienteApi.Conversores
{
    public class ConversorDeEnumParaString<TEnum> : ValueConverter<TEnum, string>
        where TEnum : struct
    {
        public ConversorDeEnumParaString() : base(
            value => value.ToString(),
            value => (TEnum)System.Enum.Parse(typeof(TEnum), value))
        {

        }
    }
}
