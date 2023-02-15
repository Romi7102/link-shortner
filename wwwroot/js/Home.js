const copy = document.querySelector("#Copy");
const formEL = document.querySelector("#form");
const output = document.querySelector("#output");
const input = document.querySelector("#input");

copy.addEventListener("click", copyToClipboard);

input.addEventListener("keyup", () => {
    if (!formEL.classList.contains("was-validated")) {
        formEL.classList.add("was-validated");
    }
});
formEL.addEventListener("submit", async (event) => {
    event.preventDefault();
    event.stopPropagation()
    const formData = new FormData(formEL);
    const url = formData.get("input");
    formEL.classList.add("was-validated");
    
    output.value = "Please wait...";
    const response = await fetch("/api/shorten", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(url),
    });

    if (response.status === 400) {
        output.value =
            "an error occurred while shortening the url , please tru again";
    } else {
        const data = await response.json();
        output.value = window.location.origin +"/s/" + data.code;
    }
    
});

function copyToClipboard() {
    output.select();
    output.setSelectionRange(0, 99999);

    navigator.clipboard.writeText(output.value);
}


