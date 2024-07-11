const convertBtn = document.getElementById('convertBtn');
convertBtn.addEventListener('click', convert);

function addOption(text) {
    const select = document.getElementById("list-box");
    const option = document.createElement("option");
    option.text = text;
    option.value = text;
    select.appendChild(option);
}

function convert() {
    const fileInput = document.getElementById('fileInput');
    const file = fileInput.files[0];
    if (!file) {
        alert('Please select a file');
        return;
    }

    const reader = new FileReader();
    reader.onload = function (e) {
        const iniText = e.target.result;
        const iniLines = iniText.split('\n');
        const iniData = {};
        let currentSection = null;

        for (const line of iniLines) {
            const trimmedLine = line.trim();
            if (trimmedLine.startsWith('[') && trimmedLine.endsWith(']')) {
                currentSection = trimmedLine.slice(1, -1);
                iniData[currentSection] = {};
            } else if (currentSection) {
                const [key, value] = trimmedLine.split('=');
                iniData[currentSection][key.trim()] = value;
            }
        }

        const xmlLines = ['<Ped chance=\"100\" '];
        for (const section in iniData) {
            xmlLines.push(`  <${section}>`);
            for (const key in iniData[section]) {
                xmlLines.push(`    <${key}>${iniData[section][key]}</${key}>`);
            }
            xmlLines.push(`  </${section}>`);
        }
        xmlLines.push('</Ped>');

        const output = document.getElementById('output');
        output.textContent = xmlLines.join('\n');
        addOption();
    };
    reader.readAsText(file);
}