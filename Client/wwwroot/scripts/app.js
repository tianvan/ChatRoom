window.ClearInput = (input) => {
    input.value = '';
};

window.ScrollBottom = (messagesBox) => {
    messagesBox.scrollTop = messagesBox.scrollHeight;
};