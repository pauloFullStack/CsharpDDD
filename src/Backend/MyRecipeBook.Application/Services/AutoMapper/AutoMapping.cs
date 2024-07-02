using AutoMapper;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            // Exemplo de como mapear a propriedade do Modelo 'RequestRegisterUserJson' caso a propriedade fosse diferente da propriedade da Entidade 'User' no exemplo 'User' tem a propriedade 'Password' e no modelo 'PasswordRegisterUserJson' e quero mapear o 'PasswordRegisterUserJson' para 'Password' abaixo o 'ForMember' esta fazendo isso, sempre o primeiro parametro lambda vai ser os dados do modelo de entrada que aqui no caso é 'RequestRegisterUserJson' e o segundo é da entidade no caso 'User', exemplo do codigo abaixo:  

            //CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            //    .ForMember(destino => destino.PasswordRegisterUserJson, opt => opt.MapFrom(sourcec => sourcec.Password));


            // ForMember é configurações extras, caso vc queria configurar algo, no caso abaixo ele ignora o Password do Modelo 'autoMapper', porque ela não esta criptografada, mas pode fazer varias configurações, como o exemplo acima de quando os nomes das propriedades dos modelos forem diferentes
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(destino => destino.Password, opt => opt.Ignore());




        }

    }
}
