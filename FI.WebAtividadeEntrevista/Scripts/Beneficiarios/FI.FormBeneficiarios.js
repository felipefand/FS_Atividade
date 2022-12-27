function prepareIncludeButton() {
    $("#formBeneficiario").submit(function (e) {
        e.preventDefault();

        var urlPost = "/beneficiario/incluir/";
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#beneficiario-nome").val(),
                "CPF": $(this).find("#beneficiario-cpf").val(),
                "IDCLIENTE": obj.Id
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON.replace("\r\n", "<br>"));
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formBeneficiario")[0].reset();
                    if (document.getElementById("gridBeneficiarios"))
                        $('#gridBeneficiarios').jtable('load');
                }
        });
    })
}

function excluirBeneficiario(id) {
    $.ajax({
        url: urlBeneficiarioDeletar,
        method: "POST",
        data: {
            "ID": id
        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON.replace("\r\n", "<br>"));
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                ModalDialog("Sucesso!", r)
                if (document.getElementById("gridBeneficiarios"))
                    $('#gridBeneficiarios').jtable('load');
            }
    });
}

function alterarBeneficiario(id) {

    $.ajax({
        url: urlBeneficiarioAlteracao,
        method: "POST",
        data: {
            "NOME": $("#beneficiario-nome").val(),
            "CPF": $("#beneficiario-cpf").val(),
            "ID": id,
            "IDCLIENTE": obj.Id
        },
        error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
        success:
            function (r) {
                ModalDialog("Sucesso!", r);
                if (document.getElementById("gridBeneficiarios"))
                    $('#gridBeneficiarios').jtable('load');
            }
    });
}