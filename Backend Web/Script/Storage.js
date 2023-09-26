
function SetLStorage(Key,Value) {
    localStorage.setItem(Key, Value);
}

function GetLStorage(Key) {
    if (localStorage.getItem(Key) == null) {
        return '';
    }
    return localStorage.getItem(Key);
}

function DelLStorage(Key)
{
    localStorage.removeItem(Key);
}

function DelLStorage(Key) {
    localStorage.removeItem(Key);
}

function ClearLStorage() {
    localStorage.clear();
}

function GetSStorage(Key) {
    if (sessionStorage.getItem(Key) == null) {
        return '';
    }
    return sessionStorage.getItem(Key);
}

function DelSStorage(Key) {
    sessionStorage.removeItem(Key);
}

function DelSStorage(Key) {
    sessionStorage.removeItem(Key);
}

function ClearSStorage() {
    sessionStorage.clear();
}