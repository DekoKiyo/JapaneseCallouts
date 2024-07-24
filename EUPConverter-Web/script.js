const params = [
    `Hat`,
    `Glasses`,
    `Ear`,
    `Watch`,
    `Mask`,
    `Top`,
    `UpperSkin`,
    `Decal`,
    `UnderCoat`,
    `Pants`,
    `Shoes`,
    `Accessories`,
    `Armor`,
    `Parachute`,
];

const models = {
    "Hat": "comp_mask_model",
    "Glasses": "comp_upperskin_model",
    "Ear": "comp_pants_model",
    "Watch": "comp_parachute_model",
    "Mask": "comp_shoes_model",
    "Top": "comp_accessories_model",
    "UpperSkin": "comp_undercoat_model",
    "Decal": "comp_armor_model",
    "UnderCoat": "comp_decal_model",
    "Pants": "comp_top_model",
    "Shoes": "prop_hat_model",
    "Accessories": "prop_glasses_model",
    "Armor": "prop_ear_model",
    "Parachute": "prop_watch_model",
};

const textures = {
    "Hat": "comp_mask_texture",
    "Glasses": "comp_upperskin_texture",
    "Ear": "comp_pants_texture",
    "Watch": "comp_parachute_texture",
    "Mask": "comp_shoes_texture",
    "Top": "comp_accessories_texture",
    "UpperSkin": "comp_undercoat_texture",
    "Decal": "comp_armor_texture",
    "UnderCoat": "comp_decal_texture",
    "Pants": "comp_top_texture",
    "Shoes": "prop_hat_texture",
    "Accessories": "prop_glasses_texture",
    "Armor": "prop_ear_texture",
    "Parachute": "prop_watch_texture",
};

const convertBtn = document.getElementById('convertBtn');
convertBtn.addEventListener('click', convert);

const genderSelection = document.getElementById('gender-list-box');
genderSelection.onchange = function () {
    const value = this.value;
    switchGender(value);
}

function convert() {
    const fileInput = document.getElementById('fileInput');
    const file = fileInput.files[0];
    if (!file) {
        alert('Please select a file first!');
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

        for (const section in iniData) {
            let xmlLines = ``;
            const gender = iniData[section]["Gender"];
            xmlLines += `<!-- ${section} -->\n<Ped chance=\"100\"`;
            for (const key in iniData[section]) {
                if (params.includes(key)) {
                    let data = iniData[section][key];
                    let comps = data.split(`:`);
                    if (comps[1] != "0") {
                        xmlLines += ` ${models[key]}=\"${comps[0]}\"`;
                    }
                    if (comps[1] != "0") {
                        xmlLines += ` ${textures[key]}=\"${comps[1]}\"`;
                    }
                }
            }
            xmlLines += `>`;
            if (gender == "Male") {
                xmlLines += "MP_M_FREEMODE_01";
            }
            else if (gender == "Female") {
                xmlLines += "MP_F_FREEMODE_01";
            }
            xmlLines += `</Ped>\n`;

            const output = document.getElementById('xml');
            const div = document.createElement("div");
            div.className = `output ${gender}`;

            const title = document.createElement("p");
            title.className = `${section} -title`;
            title.innerText = section;

            const div2 = document.createElement("div");
            div2.className = "block";

            const button = document.createElement("button");
            button.className = "copy-btn";
            button.id = `${section} -copy`;
            button.innerText = "Copy";

            const textarea = document.createElement("textarea");
            textarea.type = "text";
            textarea.className = "preview";
            textarea.id = `${section}-preview`;
            textarea.wrap = "off";
            textarea.readOnly = true;
            textarea.value = xmlLines;

            div2.appendChild(button);
            div2.appendChild(textarea);

            div.appendChild(title);
            div.appendChild(div2);
            output.appendChild(div);

            console.log(`${section} done!`);
        }
        switchGender("Male");
    };
    reader.readAsText(file);
}

function switchGender(gender) {
    const elements = document.getElementById('xml').getElementsByClassName("output");
    for (let element of elements) {
        element.classList.add("hide");
    }
    const newElements = document.getElementById('xml').getElementsByClassName(gender);
    for (let element of newElements) {
        element.classList.remove("hide");
    }
}