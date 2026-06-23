🟢 1. Caminho Feliz (Sucesso)
[x] WithValidData_ReturnsValid

Cenário: Todos os dados de usuário válidos, e-mail/CPF inéditos na tabela Users e ClinicId = 1 existente no banco.

Assert: Assert.True(result.IsValid)

🔴 2. Validações de Nome (Name)
[ ] WithEmptyName_ReturnsValidationError

Cenário: Name = "" ou null

Assert: Falha na propriedade "Name" com a mensagem "O nome de usuario não pode ser vazio"

[x] WithNameTooShort_ReturnsValidationError

Cenário: Name = "Bob" (menos de 4 caracteres)

Assert: Falha na propriedade "Name" com a mensagem "Nome deve ter entre 4 e 255 caracteres"

[x] WithNameTooLong_ReturnsValidationError

Cenário: Name com 256 caracteres.

Assert: Falha na propriedade "Name"

🔴 3. Validações de E-mail (Email)
[x] WithEmptyEmail_ReturnsValidationError

Cenário: Email = "" ou null

[x] WithInvalidEmailFormat_ReturnsValidationError

Cenário: Email = "usuario_sem_arroba.com"

Assert: Falha na propriedade "Email" com a mensagem "Endereco de email invalido"

[x] WithDuplicateEmail_ReturnsValidationError

Cenário: Email de um usuário que já existe cadastrado na tabela de Users do banco.

Assert: Falha na propriedade "Email" com a mensagem "Email de usuario ja cadastrado"

🔴 4. Validações de Telefone (Phone)
[x] WithEmptyPhone_ReturnsValidationError

Cenário: Phone = "" ou null

[x] WithPhoneTooShort_ReturnsValidationError

Cenário: Phone = "119999" (menos de 10 dígitos)

Assert: Falha na propriedade "Phone" com a mensagem "O telefone deve ter 10 ou 11 digitos"

[x] WithPhoneTooLong_ReturnsValidationError

Cenário: Phone = "1199999888877" (mais de 11 dígitos)

[x] WithFormattedPhone_ReturnsValidationError

Cenário: Phone = "(11) 99999-8888" (Regex espera apenas números puros)

🔴 5. Validações de CPF (Cpf)
[x] WithEmptyCpf_ReturnsValidationError

Cenário: Cpf = "" ou null

[x] WithCpfContainingLettersOrFormatting_ReturnsValidationError

Cenário: Cpf = "123.456.789-10" ou "1234567891a"

Assert: Falha na propriedade "Cpf" com a mensagem "O cpf deve ter 11 digitos"

[x] WithDuplicateCpf_ReturnsValidationError

Cenário: Cpf de um usuário que já existe na tabela de Users.

Assert: Falha na propriedade "Cpf" com a mensagem "Cpf de usuario ja cadastrado"

🔴 6. Validações de Clínica (ClinicId)
[x] WithEmptyClinicId_ReturnsValidationError

Cenário: ClinicId = 0 (ou vazio)

Assert: Falha na propriedade "ClinicId" com a mensagem "O ID da clinica deve ser preenchido"

[x] WithNonExistentClinicId_ReturnsValidationError

Cenário: ClinicId = 9999 (ID inexistente na tabela Clinics)

Assert: Falha na propriedade "ClinicId" com a mensagem "A clinica informada não existe"

🔴 7. Validações de Senha (Password) — Novo!
[x] WithEmptyPassword_ReturnsValidationError

Cenário: Password = "" ou null

Assert: Falha na propriedade "Password" com a mensagem "A senha nao pode ser vazia"

[x] WithPasswordTooShort_ReturnsValidationError

Cenário: Password = "1234567" (7 caracteres - menos que o mínimo de 8)

Assert: Falha na propriedade "Password" com a mensagem "Senha precisa ter 8 caracteres com no maximo 32 caracteres"

[x] WithPasswordTooLong_ReturnsValidationError

Cenário: Password com 33 caracteres (mais que o máximo de 32).

Assert: Falha na propriedade "Password"

8. Validações de Perfil de Acesso (Role) — Atualizado para Enum
[ ] WithDefaultRole_ReturnsValidationError

Cenário: Passar o valor padrão do enum (geralmente 0 ou o primeiro item da lista se ele representar um estado inválido/não definido, ex: UserRole.None).

Assert: Falha na propriedade "Role" com a mensagem "Role é obrigatório".

[ ] WithInvalidEnumValue_ReturnsValidationError

Cenário: Enviar um número que não existe no seu Enum (ex: converter o número 99 para o seu tipo enum: (UserRole)99).

Assert: Falha na propriedade "Role". (Nota: Se o seu validador usar apenas .NotEmpty(), esse teste vai falhar/passar batido. É o teste perfeito para pegar essa brecha!).