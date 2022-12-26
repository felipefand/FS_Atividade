
$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "CPF": $(this).find("#CPF").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val()
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
                $("#formCadastro")[0].reset();
            }
        });
    })

    $('#beneficiarios').on('click', () => {
        var form = '<form id="formBeneficiario" method="post">                                          ' +
            '   <div class="row align-items-end mb-2">                                                  ' +
            '       <div class="col-md-5">                                                              ' +
            '           <label for="CPF">CPF:</label>                                                   ' +
            '           <input id="beneficiario-cpf" required="required" type="text" class="form-control" id="CPF" name="CPF" placeholder="Ex.: 010.011.111-00" maxlength="14">' +
            '       </div>                                                                              ' +
            '       <div class="col-md-5">                                                              ' +
            '           <label for="Nome">Nome:</label>                                             ' +
            '           <input id="beneficiario-nome" required="required" type="text" class="form-control" id="Nome" name="Nome" placeholder="Ex.: João" maxlength="50">' +
            '       </div>                                                                              ' +
            '       <div class="col-md-2">                                                              ' +
            '           <button id="beneficiario-btn" type="submit" class="btn btn-sm btn-success">Incluir</button>           ' +
            '       </div>                                                                              ' +
            '   </div>                                                                                  ' +
            '</form>'

        ModalDialog("Beneficiários", form);

        $('#formBeneficiario').submit(function (e) {
            e.preventDefault();

            var urlPost = "/beneficiario/incluir/";

            $.ajax({
                url: urlPost,
                method: "POST",
                data: {
                    "NOME": $(this).find("#beneficiario-nome").val(),
                    "CPF": $(this).find("#beneficiario-cpf").val()
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
                    }
            });
        })
    })
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}