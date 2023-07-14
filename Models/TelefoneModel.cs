using ParanaBancoClienteApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ParanaBancoClienteApi.Models
{
    public class TelefoneModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? DDD { get; set; }

        public string? Numero { get; set; }

        public TipoTelefone TipoTelefone { get; set; }


        public int ClienteId { get; set; }

        public ClienteModel? Cliente { get; set; }

        public string ObterDescricaoEnum(TipoTelefone tipoTelefone) 
        {
            var tipo = tipoTelefone.GetType();
            var membro = tipo.GetMember(tipoTelefone.ToString());

            if (membro.Length > 0)
            {
                var atributoDescricao = membro[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                if (atributoDescricao != null)
                {
                    return atributoDescricao.Description;
                }
            }

            return tipoTelefone.ToString();
        }
    }
}
