
$(document).ready(function () {
    $.fn.dataTable.ext.errMode = 'throw';
    tournamentsDatatable = $('#tournamentsDatatable').DataTable({

        "ajax": {

            "processing": true,

            "serverSide": true,

            "url": '/tournament/get',

            "dataSrc": function (json) {
                for (var i = 0; i < json.tournaments.length; i++) {
                    json.tournaments[i].userType = json.userType;
                }
                return json.tournaments;
            },

            "dataType": "json",

            "type": "GET",

            "crossDomain": true,



        },

        "bDestroy": true,
        "columnDefs": [
            {
                "targets": [0],
                "visible": false
            }
        ],
        "columns": [

            { "title": "Id","data": "id", "name": "Id", "autoWidth": true },
            { "title": "Tournament Name", "data": "tournamentName", "name": "Tournament Name", "autoWidth": true },
            { "title": "Start Time","data": "startTime", "name": "Start Time", "autoWidth": true },
            { "title": "Minimum Players","data": "minimumPlayers", "name": "Minimum Players", "autoWidth": true },
            { "title": "Maximum Players", "data": "maximumPlayers", "name": "Maximum Players", "autoWidth": true },
            { "title": "Waiting Players","data": "waitingPlayers", "name": "Waiting Players", "autoWidth": true },
            { "title": "Playing Players", "data": "playingPlayers", "name": "Playing Players", "autoWidth": true },
            { "title": "Status", "data": "status", "name": "Status", "autoWidth": true },
            {
                data: null,
                "render": function (data, type, row) {
                    return "<a href='#' class='btn btn-primary' onclick=JoinTournament('" + row.id + "'); >Join</a>";
                }
            },
            {
                "render": function (data, type, row) {
                    if (row.userType==1) {
                        return "<a href='#' class='btn btn-danger' onclick=DeleteTournament('" + row.id + "'); >Delete</a>";
                    }
                    return "";
                    
                }
            }

        ],
        rowGroup: {

            startRender: null,

            endRender: function (rows, group) {

                return group + ' (' + rows.count() + ')';

            }

        }

    });



});  