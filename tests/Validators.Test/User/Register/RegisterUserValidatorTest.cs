using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using FluentAssertions;
using MyRecipeBook.Exceptions;

namespace Validators.Test.User.Register
{
    // TODO TESTE É INDEPENDENTE POR ISSO NÃO USAR O CONSTRUTOR
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }


        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.NAME_EMPTY));

        }


        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.EMAIL_EMPTY));

        }


        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "email.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.EMAIL_INVALID));

        }


        // [Theory] = é usado para quando vc quiser passar parametros
        // [InlineData(0, true, 5.4f, "teste")] = serão os parametros passados, ai so criar os argumentos na função => Error_Password_Invalid(string arg1, int arg2, bool arg3)...

        // nesse caso abaixo esta sendo testado o erro de o usuario mandar menos de '6' caracteres no campo da senha, ele vai fazer tipo um loop testando as variações de tamanho de caracteres '1,2,3,4,5,', vão ser testados todos esses tamanhos de caracteres e todos devem dar erro, então essa é a finalidade de 'Theory' e o 'InlineData' = o valor do parametro 'int passwordLength'
        [Theory]
        // pode se passar mais parametros
        //[InlineData(0, true, 5.4f, "teste")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessageException.PASSWORD_EMPTY));

        }

    }
}
