var response = [];
var lastInsert = {};
var lastId = " ";
var id = "";
var count = 0;

function callBackData() {
    $.ajax({
        type: "GET",
        url: 'api/persons',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (item) {
            response.push.apply(response, item);
            lastInsert = response.pop()
            lastId = lastInsert.id.toString();
            id = lastId;
        },
        error: function () {
            alert("Error: Data not received");
        }
    });
}

function clearFields() {
    document.getElementById('name').value = "";
    document.getElementById('my_select').value = "";
    $('#checkBox').prop('checked', false);
};

$(function () {
    $('.messageName, .messageSelect, .messageConsent, .messageSent, .messageUpdate').hide();

    $('form').on('submit', function (e) {
        e.preventDefault();

        function fillFields() {
            $('#name').val(nameValue);
            $('#my_select').val(selectValue);
            $('#checkBox').prop('checked', true);
        };

        var selectValue = [];
        var nameValue = document.getElementById("name").value;
        var consentValue = document.getElementById("checkBox").checked;

        $("#my_select option").each(function() {
            if (this.selected == true) {
                selectValue.push(this.value);
            }
        });

        var dataForPut =
        {
            id: lastId,
            personName: nameValue,
            selectionName: selectValue.toString(),
            consent: consentValue
        };


        var dataForPost =
        {
            personName: nameValue,
            selectionName: selectValue.toString(),
            consent: consentValue
        };

        if (nameValue == "" || consentValue == false || selectValue == "") {
            $('.messageSent, .messageUpdate').hide();
            $('.btn').each(function () {
                var nameData = $(this).val();
                var nameCheck = nameData.length;

                if (nameCheck < 2) {
                    $(this).parent().find('.messageName').show();
                } else {
                    $(this).parent().find('.messageName').hide();
                }
            });

            $('.select').each(function () {
                var selectData = $(this).val();
                var selectCheck = selectData.length;

                if (selectCheck < 1) {
                    $(this).parent().find('.messageSelect').show();
                } else {
                    $(this).parent().find('.messageSelect').hide();
                }
            });

            var check = $('input[type=checkbox]').prop('checked');
            if (check == false) {
                $(this).parent().find('.messageConsent').show();
            } else {
                $(this).parent().find('.messageConsent').hide();
            }
            return false;
        } else {
            if (id == lastId) {
                $('.messageName, .messageSelect, .messageConsent').hide();

                $.ajax({
                    type: "PUT",
                    url: 'api/persons/' + id,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(dataForPut),
                    success: function () {
                        $('.messageSent').hide();
                        clearFields();
                        fillFields();
                        $('.messageUpdate').show();
                    },
                    error: function () {
                        alert("Error: Data not updated");
                    }
                });
            } else {
                $('.messageName, .messageSelect, .messageConsent').hide();

                $.ajax({
                    type: "POST",
                    url: 'api/persons/',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(dataForPost),
                    success: function () {
                        $('.messageSent').show();
                        callBackData();
                        clearFields();
                        fillFields();
                    },
                    error: function () {
                        alert("Error: Data not updated");
                    }
                });
            }
        }
    });
});