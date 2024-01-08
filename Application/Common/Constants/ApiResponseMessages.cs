namespace Application.Common.Constants;
public static class ApiResponseMessages
{
    public const string User = "Usuário";
    public const string UserIdRequired = "O id do usuário é obrigatório.";
    public const string Password = "Senha";
    public const string YouDoNotHavePermission = "Você não tem permissão";
    public const string PasswordMostHaveAtLeastOneNumber = "A senha tem que ter pelo menos um número.";
    public const string PasswordMostHaveAtLeastOneUpperLetter = "A senha tem que ter pelo menos uma letra maiúscula.";
    public const string InvalidDocument = "CNPJ inválido.";
    public const string PasswordMostHaveAtLeastOneLowerLetter = "A senha tem que ter pelo menos uma letra minúscula.";
    public const string PasswordMostHaveAtLeastCharactersLong = "A senha tem que ter pelo menos 8 caracteres.";
    public const string PasswordMostHaveAtLeastSpecialCaracter = "A senha tem que ter pelo um caracter especial.";
    public const string PasswordAreNotTheSame = "As senhas não são iguais.";
    public const string UnableToaAcceptYouRequest = "Não foi possível aceitar sua solicitação, tente novamente mais tarde.";
    public const string UserOrEmail = "Email ou senha";
    public const string PasswordOrEmailInvalid = "E-mail e/ou senha inválidos.";
    public const string UserDisabled = "Usuário desativado.";
    public const string UserNotFound = "Usuário não encontrado.";
    public const string InvalidActualPassword = "Senha atual inválida.";

    public const string Document = "CNPJ";
    public const string CompanyAlreadyRegistered = "Empresa já cadastrada.";
    public const string CompanyNotFound = "Empresa não encontrada.";
    public const string Url = "Url";
    public const string UrlAlreadyUsed = "Essa url já está sendo utilizada.";
    public const string FileNotAllowed = "Arquivo não permitido.";
    public const string UnableUpload = "Não foi possível fazer o upload.";

    public const string Company = "Empresa.";
    public const string DocumentAlreadyRegistered = "CNPJ já cadastrado.";

    public const string EmailAlreadyRegistered = "E-mail já cadastrado.";
    public const string Role = "Permissão";
    public const string RoleNotFound = "Permissão não encontra.";

    public const string EnterAName = "Você tem que digitar um nome.";
    public const string EnterAPassword = "Você tem que digitar uma senha.";
    public const string EnterACompanyName = "Você tem que digitar a razão social.";
    public const string EnterATradingName = "Você tem que digitar o nome fantasia.";
    public const string EnterASegment = "Você tem que digitar um segmento.";
    public const string EnterADocument = "Você tem que digitar um cnpj.";
    public const string EnterAUrl = "Você tem que digitar uma url.";
    public const string InvalidUrl = "Url inválida. Url só podem conter letras minúsculas, números e hifens. Eles devem começar e terminar com uma letra ou número.";

    public const string SelectStartingMonthPayment = "Você tem que selecionar o mês início de pagamento.";
    public const string EnterNumberOfColaborators = "Você tem que digitar a quantidade de colaboradores.";
    public const string EnterNumberMonthsContract = "Você tem que digitar a quantidade de mês do contrato.";
    public const string LeastOnePerson = "Você tem que enviar pelo menos 1 responsável.";
    public static string DuplicateEmail(string duplicateEmail) => $"E-mail(s) duplicado(s): {duplicateEmail}";
    public const string EnterResponsibleName = "Você tem que digitar o nome do responsável.";
    public const string EnterResponsibleEmail = "Você tem que digitar o e-mail do responsável.";
    public const string EnterResponsibleCellphone = "Você tem que digitar o telefone do responsável.";

    public const string InvalidEmail = "E-mail inválido.";
}
