$(document).ready(function() {
    HideProgress();
    HideMessageBox();
    HideSessionExpired();
});

//ProgressBar
function ShowProgress() {
    document.getElementById("ProgressBar").style.display = 'block';
}

function HideProgress() {
    document.getElementById("ProgressBar").style.display = 'none';
}
function ShowProgressExtended() {
    document.getElementById("ProgressBarExtended").style.display = 'block';
}
function HideProgressExtended() {
    document.getElementById("ProgressBarExtended").style.display = 'none';
}
window.onkeyup = function (event) {
    if (event.keyCode == 27) {
        HideMessageBox();
    }
}
//MessageBox
function MsgBox(msgtype, title, msg) //msgtype : S, E, W, I
{
    HideProgress();
    if (msgtype == "S" || msgtype == "s") {
        ShowMessageBox();
        $('#ModalContent').css({
            "border-color": "#d0cece",
            "border-width": "3px",
            "border-style": "solid"
        });
        document.getElementById("Msg-Icon").src = $("#Icon_Success").val();
        document.getElementById("Msgheader").style.backgroundColor = '#d0cece';
        document.getElementById("Msgtitle").textContent = title;
        //document.getElementById("Msgok").className = '';
        //document.getElementById("Msgok").className = 'btn btn-sm btn-success';
        document.getElementById("Msg-body").textContent = msg;
        document.getElementById("Msg-body").style.Color = 'Green';
    }
    else if (msgtype == "E" || msgtype == "e") {
        ShowMessageBox();
        $('#ModalContent').css({
            "border-color": "#d0cece",
            "border-width": "3px",
            "border-style": "solid"
        });
        document.getElementById("Msg-Icon").src = $("#Icon_Error").val();
        document.getElementById("Msgheader").style.backgroundColor = '#d0cece';
        document.getElementById("Msgtitle").textContent = title;
        document.getElementById("Msg-body").textContent = msg;
        document.getElementById("Msg-body").style.color = 'Red';
    }
    //if (msgtype == "S" || msgtype == "s") {
    //    ShowMessageBox();
    //    $('#ModalContent').css({
    //        "border-color": "#449d44",
    //        "border-width": "3px",
    //        "border-style": "solid"
    //    });
    //    document.getElementById("Msg-Icon").src = $("#Icon_Success").val();
    //    document.getElementById("Msgheader").style.backgroundColor = '#449d44';
    //    document.getElementById("Msgtitle").textContent = title;
    //    document.getElementById("Msgok").className = '';
    //    document.getElementById("Msgok").className = 'btn btn-sm btn-success';
    //    document.getElementById("Msg-body").textContent = msg;
    //    document.getElementById("Msg-body").style.Color = 'black';
    //}
    //else if (msgtype == "E" || msgtype == "e") {
    //    ShowMessageBox();
    //    $('#ModalContent').css({
    //        "border-color": "#c9302c",
    //        "border-width": "3px",
    //        "border-style": "solid"
    //    });
    //    document.getElementById("Msg-Icon").src = $("#Icon_Error").val();
    //    document.getElementById("Msgheader").style.backgroundColor = '#c9302c';
    //    document.getElementById("Msgtitle").textContent = title;
    //    document.getElementById("Msgok").className = '';
    //    document.getElementById("Msgok").className = 'btn btn-sm btn-danger';
    //    document.getElementById("Msg-body").textContent = msg;
    //    document.getElementById("Msg-body").style.Color = 'black';
    //}
    else if (msgtype == "W" || msgtype == "w") {
        ShowMessageBox();
        $('#ModalContent').css({
            "border-color": "#FF8800",
            "border-width": "3px",
            "border-style": "solid"
        });
        document.getElementById("Msg-Icon").src = $("#Icon_Warning").val();
        document.getElementById("Msgheader").style.backgroundColor = '#FF8800';
        document.getElementById("Msgtitle").textContent = title;
        document.getElementById("Msgok").className = '';
        document.getElementById("Msgok").className = 'btn btn-sm btn-warning';
        document.getElementById("Msg-body").textContent = msg;
        document.getElementById("Msg-body").style.Color = 'black';
    }
    else if (msgtype == "I" || msgtype == "i") {
        ShowInfo_MessageBox();
        $('#ModalContent').css({
            "border-color": "#46b8da",
            "border-width": "3px",
            "border-style": "solid"
        });
        document.getElementById("infoMsg-Icon").src = $("#Icon_Info").val();
        document.getElementById("infoMsgheader").style.backgroundColor = '#d0cece';
        document.getElementById("infoMsgtitle").textContent = title;
        //document.getElementById("Msgok").className = '';
        //document.getElementById("Msgok").className = 'btn btn-sm btn-success';
        document.getElementById("infoMsg-body").textContent = msg;
        document.getElementById("infoMsg-body").style.Color = 'Green';
    }
    else {
        HideInfo_MessageBox();
    }
}

function ShowMessageBox() {
    document.getElementById("MessageBox").style.display = 'block';
}

function HideMessageBox() {
    document.getElementById("MessageBox").style.display = 'none';

}

function ShowInfo_MessageBox() {
    document.getElementById("InfoMessageBox").style.display = 'block';

}

function HideInfo_MessageBox() {
    document.getElementById("InfoMessageBox").style.display = 'none';

}
function ShowSessionExpired() {
    $('#SessionExpiry').show();
}

function HideSessionExpired() {
    $('#SessionExpiry').hide();
}

function SessionTimeOut(msgtype, title, msg) {
    if (msgtype == "E" || msgtype == "e") {
        ShowSessionExpired();
        $('#ModalContentSession').css({
            "border-color": "#c9302c",
            "border-width": "3px",
            "border-style": "solid"
        });
        document.getElementById("SessionIcon").src = $("#Icon_Error").val();
        document.getElementById("Sessionheader").style.backgroundColor = '#c9302c';
        document.getElementById("Sessiontitle").textContent = title;
        document.getElementById("Btn_Yes").className = '';
        document.getElementById("Btn_Yes").className = 'btn btn-sm btn-danger';
        document.getElementById("Btn_No").className = '';
        document.getElementById("Btn_No").className = 'btn btn-sm btn-danger';
        // document.getElementById("Sessionbody").textContent = msg;
        document.getElementById("Sessionbody").style.Color = 'black';
    }
}

// Yes BUTTON FUNCTIONS
$(document).on('click', '#Btn_Yes', function (e) {
    e.preventDefault();
    ResetClientSideSessionTimers();
    $('#SessionExpiry').hide();
    
});

// No BUTTON FUNCTIONS
$(document).on('click', '#Btn_No', function (e) {
    Msgok
    e.preventDefault();
    ResetClientSideSessionTimers();
    $('#SessionExpiry').hide();
    //window.location.href = $("#Login").val();   
});

function ReloadPage() {
    window.location.reload(true);
}

//Invoice view click 
function showInvoiceCust() {
    HideInfo_MessageBox();
    document.getElementById("myModal").style.display = "block";
}

function closeForm() {
    ReloadPage()
    document.getElementById("myModal").style.display = "none";
}