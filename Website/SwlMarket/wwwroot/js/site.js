window.onload = () => updateTimeSince();

function updateTimeSince() {
    let current = new Date().getTime();
    for (let element of document.getElementsByClassName("timesince")) {
        let timestamp = Date.parse(element.textContent);
        let minutesDiff = (current - timestamp) / 1000 / 60;
        if (minutesDiff < 0) {
            element.textContent = `A short time ago`;
            continue;
        }
        if (minutesDiff < 60) {
            element.textContent = `${Math.trunc(minutesDiff)} minutes ago`;
            continue;
        }
        let hoursDiff = minutesDiff / 60;
        if (hoursDiff < 24) {
            element.textContent = `${Math.trunc(hoursDiff)} hours ago`;
            continue;
        }
        let daysAgo = hoursDiff / 24;
        element.textContent = `${Math.trunc(daysAgo)} days ago`;
    }
}