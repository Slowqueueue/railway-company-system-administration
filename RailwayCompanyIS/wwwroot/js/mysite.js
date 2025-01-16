
function showCustomAlert() {
    var customAlert = document.getElementById('customAlert');
    var overlay = document.getElementById('overlay');
    var departure_sound = document.getElementById('departure_sound');

    customAlert.style.display = 'block';
    overlay.style.display = 'block';

    departure_sound.muted = false;
}

function hideCustomAlert() {
    var customAlert = document.getElementById('customAlert');
    var overlay = document.getElementById('overlay');
    var departure_sound = document.getElementById('departure_sound');

    customAlert.style.display = 'none';
    overlay.style.display = 'none';

    departure_sound.muted = true;
}