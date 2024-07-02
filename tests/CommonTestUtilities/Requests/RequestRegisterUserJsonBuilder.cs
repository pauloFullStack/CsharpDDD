using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {

        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            // OBS: na função 'RuleFor' o primeiro argumento é o campo que sera setado o valor fake, o segundo argumento é o valor gerado aleatorio pelo pacote 'Bogus' onde vc excolhe qual objeto acessar e gerar o valor aleatorio

            // OBS: o primeiro argumento da função 'RuleFor' é o 'Name' é da instancia(RequestRegisterUserJson) criada pelo 'Faker' do pacote 'Bogus' e o segundo argumento o valor gerado pelo pacote 'Bogus'

            // OBS: o 'f' do parametro da função lambda, acessa os dados aleatorios do pacote 'Bogus', no caso objetos e seus atributos e funções Objetos => 'Person' e 'Internet' | Atibuto =>  'FirstName' e Função => 'Email'

            // OBS: na segunda 'RuleFor' e passa mais um parametro na função lambda, no caso o segundo parametro ele acessa a instancia criada por 'Fake' que no caso é '<RequestRegisterUserJson>' onde 'user.Name' já foi atribuido na primeira 'RuleFor' , ele consegue pegar o valor gerado aleatorio 'f.Person.FirstName' e passar como parametro para a função 'f.Internet.Email(user.Name_valorJaSetadoPeloPacoete)'

            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, (f) => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
        }

    }
}
