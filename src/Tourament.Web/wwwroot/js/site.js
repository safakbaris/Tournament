var tournamentsDatatable;
function JoinTournament(tournamentId) {

    var model = {};
    model.TournamentId = parseInt(tournamentId);

    $.ajax({
        url: '/Tournament/Join',
        data: JSON.stringify(model),
        type: "POST",
        dataType: 'JSON',
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: data.message,
                    timer: 2000
                })
                tournamentsDatatable.ajax.reload();
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Warning',
                    text: data.message,
                    timer: 2000
                })
            }

        },
        error: function (passParams) {
            console.log("Error is " + passParams);
        }
    });
}

function DeleteTournament(id) {

    $.ajax({
        url: '/Tournament/Delete/' + id,
        type: "DELETE",
        dataType: 'JSON',
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: data.message,
                    timer: 2000
                })
                tournamentsDatatable.ajax.reload();
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Warning',
                    text: data.message,
                    timer: 2000
                })
            }

        },
        error: function (passParams) {
            console.log("Error is " + passParams);
        }
    });
}

function Play() {

    var gameId = $("#hdnGameId").val();
    var number = $("#txtNumberToSubstract").val();
    $.ajax({
        url: '/Game/Play?gameId=' + gameId + '&number=' + number,
        type: "POST",
        dataType: 'JSON',
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                $("#lblNumber").html(data.data);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: data.message,
                    timer: 2000
                })
            }
            else {
                Swal.fire({
                    icon: 'warning',
                    title: 'Warning',
                    text: data.message,
                    timer: 2000
                })
            }

        },
        error: function (passParams) {
            console.log("Error is " + passParams);
        }
    });
}