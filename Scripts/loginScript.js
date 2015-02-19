$('.loginModal').on('shown.bs.modal', function () {
    $('#UserNameTextBox').focus();
});

$('.addGenreModal').on('shown.bs.modal', function () {
    $('#ContentPlaceHolder1_GenreTextBox').focus();
});

function openModal() {
    $('.loginModal').modal('show');
    $('#UserNameTextBox').focus();
}

function openErrorModal() {
    $('.errorModal').modal('show');
}
