const api = "";

document.addEventListener("DOMContentLoaded", () => {
    bindForms();
    loadAll();
});

function bindForms() {
    document
        .getElementById("sensorForm")
        .addEventListener("submit", registerSensor);

    document
        .getElementById("telemetryForm")
        .addEventListener("submit", sendTelemetry);

    document
        .getElementById("uploadForm")
        .addEventListener("submit", uploadFile);
}

async function loadAll() {
    await checkApi();

    const sensors = await loadSensors();

    await loadTelemetry();
    await loadAlerts();

    populateDeviceSelects(sensors);
}

async function checkApi() {
    const status =
        document.getElementById("apiStatus");

    try {
        const response =
            await fetch(`${api}/api/health`);

        if (!response.ok) {
            throw new Error();
        }

        status.textContent = "API Online";
        status.style.borderColor = "var(--accent)";
    }
    catch {
        status.textContent = "API Offline";
        status.style.borderColor = "var(--danger)";
    }
}

async function loadSensors() {
    const response =
        await fetch(`${api}/api/sensors`);

    const sensors =
        await response.json();

    document
        .getElementById("sensorCount")
        .textContent = sensors.length;

    const container =
        document.getElementById("sensorCards");

    container.innerHTML =
        sensors.map(sensor => `
            <article class="sensor-card">

                <h4>
                    ${escapeHtml(sensor.deviceId)}
                </h4>

                <p>
                    <strong>Location:</strong>
                    ${escapeHtml(sensor.location)}
                </p>

                <p>
                    <strong>Category:</strong>
                    ${escapeHtml(sensor.category)}
                </p>

                <p>
                    <strong>Status:</strong>
                    ${escapeHtml(sensor.status)}
                </p>

            </article>
        `).join("");

    return sensors;
}

async function loadTelemetry() {
    const response =
        await fetch(`${api}/api/telemetry`);

    const telemetry =
        await response.json();

    document
        .getElementById("telemetryCount")
        .textContent = telemetry.length;

    const table =
        document.getElementById("telemetryTable");

    table.innerHTML =
        telemetry.map(item => `
            <tr>

                <td>
                    ${new Date(item.timestamp).toLocaleString()}
                </td>

                <td>
                    ${escapeHtml(item.deviceId)}
                </td>

                <td>
                    ${escapeHtml(item.metric)}
                </td>

                <td>
                    ${escapeHtml(item.dataType)}
                </td>

                <td>
                    ${escapeHtml(item.displayValue)}
                    ${escapeHtml(item.unit)}
                </td>

            </tr>
        `).join("");
}

async function loadAlerts() {
    const container = document.getElementById("alerts");
    const alertCount = document.getElementById("alertCount");

    try {
        const response = await fetch(`${api}/api/alerts`);

        if (!response.ok) {
            container.innerHTML =
                `<div class="alert warning">
                    Could not load alerts.
                </div>`;
            return;
        }

        const alerts = await response.json();

        alertCount.textContent =
            alerts.filter(a =>
                a.severity.toLowerCase() !== "info"
            ).length;

        container.innerHTML = "";

        alerts.forEach(alert => {
            const severity =
                alert.severity || "Info";

            const alertType =
                alert.alertType || "System Alert";

            const currentValue =
                alert.currentValue || "N/A";

            const message =
                alert.message || "";

            const suggestedAction =
                alert.suggestedAction ||
                "No action required.";

            const card =
                document.createElement("div");

            card.className =
                `alert ${severity.toLowerCase()}`;

            const title =
                document.createElement("strong");

            title.textContent =
                `${severity}  ${alertType}`;

            const device =
                document.createElement("div");

            device.textContent =
                `Device: ${alert.deviceId}`;

            const value =
                document.createElement("div");

            value.textContent =
                `Current value: ${currentValue}`;

            const messageText =
                document.createElement("div");

            messageText.textContent =
                message;

            const action =
                document.createElement("div");

            action.className =
                "alert-action";

            action.textContent =
                `Recommended action: ${suggestedAction}`;

            card.appendChild(title);
            card.appendChild(device);
            card.appendChild(value);
            card.appendChild(messageText);
            card.appendChild(action);

            container.appendChild(card);
        });
    }
    catch {
        container.innerHTML =
            `<div class="alert warning">
                Could not connect to the alerts API.
            </div>`;
    }
}
function populateDeviceSelects(sensors) {
    const options =
        sensors.map(sensor => `
            <option value="${escapeHtml(sensor.deviceId)}">
                ${escapeHtml(sensor.deviceId)}
                
                ${escapeHtml(sensor.location)}
            </option>
        `).join("");

    document
        .getElementById("telemetryDevice")
        .innerHTML = options;

    document
        .getElementById("uploadDevice")
        .innerHTML = options;
}

async function registerSensor(event) {
    event.preventDefault();

    const payload = {
        deviceId:
            document
                .getElementById("deviceId")
                .value
                .trim(),

        location:
            document
                .getElementById("location")
                .value
                .trim(),

        category:
            document
                .getElementById("category")
                .value
    };

    const message =
        document.getElementById("sensorMessage");

    message.textContent =
        "Registering sensor...";

    try {
        const response =
            await fetch(`${api}/api/sensors`, {
                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body:
                    JSON.stringify(payload)
            });

        const result =
            await response.json();

        if (!response.ok) {
            message.textContent =
                getErrorMessage(result);

            return;
        }

        message.textContent =
            `Sensor ${result.deviceId} registered successfully.`;

        event.target.reset();

        await loadAll();
    }
    catch {
        message.textContent =
            "Could not connect to the API.";
    }
}

async function sendTelemetry(event) {
    event.preventDefault();

    const payload = {
        deviceId:
            document
                .getElementById("telemetryDevice")
                .value,

        metric:
            document
                .getElementById("metric")
                .value
                .trim(),

        dataType:
            document
                .getElementById("dataType")
                .value,

        value:
            document
                .getElementById("value")
                .value
                .trim(),

        unit:
            document
                .getElementById("unit")
                .value
                .trim()
    };

    const message =
        document.getElementById(
            "telemetryMessage"
        );

    message.textContent =
        "Sending telemetry...";

    try {
        const response =
            await fetch(`${api}/api/telemetry`, {
                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body:
                    JSON.stringify(payload)
            });

        const result =
            await response.json();

        if (!response.ok) {
            message.textContent =
                getErrorMessage(result);

            return;
        }

        message.textContent =
            "Telemetry received by the Smart-X API.";

        await loadAll();
    }
    catch {
        message.textContent =
            "Could not connect to the API.";
    }
}

async function uploadFile(event) {
    event.preventDefault();

    const deviceId =
        document
            .getElementById("uploadDevice")
            .value;

    const file =
        document
            .getElementById("fileInput")
            .files[0];

    const message =
        document
            .getElementById("uploadMessage");

    if (!file) {
        message.textContent =
            "Choose a file first.";

        return;
    }

    const formData =
        new FormData();

    formData.append(
        "file",
        file
    );

    message.textContent =
        "Uploading file...";

    try {
        const response =
            await fetch(
                `${api}/api/sensors/${encodeURIComponent(deviceId)}/upload`,
                {
                    method: "POST",
                    body: formData
                }
            );

        const result =
            await response.json();

        if (!response.ok) {
            message.textContent =
                getErrorMessage(result);

            return;
        }

        message.textContent =
            `Uploaded ${result.attachment.originalFileName} successfully.`;

        event.target.reset();
    }
    catch {
        message.textContent =
            "Could not upload the file.";
    }
}

async function runDemo(
    url,
    method = "GET"
) {
    const output =
        document.getElementById("demoOutput");

    output.textContent =
        "Running demonstration...";

    try {
        const response =
            await fetch(url, {
                method: method
            });

        const result =
            await response.json();

        output.textContent =
            JSON.stringify(
                result,
                null,
                2
            );
    }
    catch {
        output.textContent =
            "Could not run demonstration.";
    }
}
async function generateMockTelemetry(count) {
    const message =
        document.getElementById("mockMessage");

    message.textContent =
        `Generating ${count} telemetry readings...`;

    try {
        const response =
            await fetch(
                `${api}/api/telemetry/mock/${count}`,
                {
                    method: "POST"
                }
            );

        const result =
            await response.json();

        if (!response.ok) {
            message.textContent =
                getErrorMessage(result);

            return;
        }

        message.textContent =
            result.message;

        await loadAll();
    }
    catch {
        message.textContent =
            "Could not generate mock telemetry.";
    }
}

function getErrorMessage(result) {
    if (result.message) {
        return result.message;
    }

    if (result.errors) {
        if (Array.isArray(result.errors)) {
            return result.errors.join(" ");
        }

        return Object
            .values(result.errors)
            .flat()
            .join(" ");
    }

    return "Something went wrong.";
}

function escapeHtml(value) {
    const text =
        String(value ?? "");

    return text
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

//Reference list
//GeeksforGeeks(2023).CSS tutorial. [online] GeeksforGeeks.Available at: https://www.geeksforgeeks.org/css/css-tutorial/ [Accessed 14 Sept. 2026].
//Microsoft.com. (2024).How to create simple HTML / CSS project in visual studio 2022 - Microsoft Q & A. [online] Available at: https://learn.microsoft.com/en-us/answers/questions/1663914/how-to-create-simple-html-css-project-in-visual-st [Accessed 14 Sept. 2026].
//RoseHJM(2026).JavaScript and TypeScript in visual studio. [online] MicrosoftLearn.Available at: https://learn.microsoft.com/en-us/visualstudio/javascript/javascript-in-visual-studio?view=visualstudio [Accessed 14 Sept. 2026].
//W3SCHOOLS(2019).CSS tutorial. [online] W3schools.com.Available at: https://www.w3schools.com/css/ [Accessed 14 Sept. 2026].W3Schools (2024). HTML tutorial. [online] W3schools.com. Available at: https://www.w3schools.com/html/ [Accessed 14 Sept. 2026].
//wadepickett(2025).Static files in ASP.NET core. [online] Microsoft.com.Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].