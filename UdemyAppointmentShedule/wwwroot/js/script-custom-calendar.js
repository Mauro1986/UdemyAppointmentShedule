var routeURL = location.protocol +"//"+ location.host;

$(document).ready(function () {
    $("#appointmentDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    InitializeCalendar();
});

function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDetail) {
    $("#appointmentInput").modal("show");
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
}

function onSubmitForm() {
    var requestData = {
        Id: parseInt($("#id").val()),
        Title: $("#title").val(),
        Description: $("#description").val(),
        StartDate: $("#appointmentDate").val(),
        Duriation: $("#duration").val(),
        DoctorId: $("#doctorId").val(),
        PatientId: $("#patientId").val(),
    };
    $.ajax({
        url: routeURL + '/api/Appointment/SaveCalendarData',
        type: 'POST',
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (resposne) {
            if (resposne.status === 1 || resposne.status === 2) {
                $.notify(resposne.message, "success");
                onCloseModal();
            }
            else {
                $.notify(resposne.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}
