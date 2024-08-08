const name = document.getElementById("name");
const model = document.getElementById("model");

const comp_mask_model = document.getElementById("comp_mask_model");
const comp_mask_texture = document.getElementById("comp_mask_texture");
const comp_upperskin_model = document.getElementById("comp_upperskin_model");
const comp_upperskin_texture = document.getElementById("comp_upperskin_texture");
const comp_pants_model = document.getElementById("comp_pants_model");
const comp_pants_texture = document.getElementById("comp_pants_texture");
const comp_parachute_model = document.getElementById("comp_parachute_model");
const comp_parachute_texture = document.getElementById("comp_parachute_texture");
const comp_shoes_model = document.getElementById("comp_shoes_model");
const comp_shoes_texture = document.getElementById("comp_shoes_texture");
const comp_accessories_model = document.getElementById("comp_accessories_model");
const comp_accessories_texture = document.getElementById("comp_accessories_texture");
const comp_undercoat_model = document.getElementById("comp_undercoat_model");
const comp_undercoat_texture = document.getElementById("comp_undercoat_texture");
const comp_armor_model = document.getElementById("comp_armor_model");
const comp_armor_texture = document.getElementById("comp_armor_texture");
const comp_decal_model = document.getElementById("comp_decal_model");
const comp_decal_texture = document.getElementById("comp_decal_texture");
const comp_top_model = document.getElementById("comp_top_model");
const comp_top_texture = document.getElementById("comp_top_texture");
const prop_hat_model = document.getElementById("prop_hat_model");
const prop_hat_texture = document.getElementById("prop_hat_texture");
const prop_glasses_model = document.getElementById("prop_glasses_model");
const prop_glasses_texture = document.getElementById("prop_glasses_texture");
const prop_ear_model = document.getElementById("prop_ear_model");
const prop_ear_texture = document.getElementById("prop_ear_texture");
const prop_watch_model = document.getElementById("prop_watch_model");
const prop_watch_texture = document.getElementById("prop_watch_texture");

const generateBtn = document.getElementById('generateBtn');
generateBtn.addEventListener('click', generate);

const output = document.getElementById('output');

function generate() {
    if (model.value == "") {
        alert("Enter model name first!");
    }
    else {
        let json = `{\n`;
        json += `  "model": ${model.value},\n`;
        if (comp_mask_model.value != "" && comp_mask_texture.value != "") {
            json += `  "comp_mask_model": ${comp_mask_model.value},\n`;
            json += `  "comp_mask_texture": ${comp_mask_texture.value},\n`;
        }
        if (comp_upperskin_model.value != "" && comp_upperskin_texture.value != "") {
            json += `  "comp_upperskin_model": ${comp_upperskin_model.value},\n`;
            json += `  "comp_upperskin_texture": ${comp_upperskin_texture.value},\n`;
        }
        if (comp_pants_model.value != "" && comp_pants_texture.value != "") {
            json += `  "comp_pants_model": ${comp_pants_model.value},\n`;
            json += `  "comp_pants_texture": ${comp_pants_texture.value},\n`;
        }
        if (comp_parachute_model.value != "" && comp_parachute_texture.value != "") {
            json += `  "comp_parachute_model": ${comp_parachute_model.value},\n`;
            json += `  "comp_parachute_texture": ${comp_parachute_texture.value},\n`;
        }
        if (comp_shoes_model.value != "" && comp_shoes_texture.value != "") {
            json += `  "comp_shoes_model": ${comp_shoes_model.value},\n`;
            json += `  "comp_shoes_texture": ${comp_shoes_texture.value},\n`;
        }
        if (comp_accessories_model.value != "" && comp_accessories_texture.value != "") {
            json += `  "comp_accessories_model": ${comp_accessories_model.value},\n`;
            json += `  "comp_accessories_texture": ${comp_accessories_texture.value},\n`;
        }
        if (comp_undercoat_model.value != "" && comp_undercoat_texture.value != "") {
            json += `  "comp_undercoat_model": ${comp_undercoat_model.value},\n`;
            json += `  "comp_undercoat_texture": ${comp_undercoat_texture.value},\n`;
        }
        if (comp_armor_model.value != "" && comp_armor_texture.value != "") {
            json += `  "comp_armor_model": ${comp_armor_model.value},\n`;
            json += `  "comp_armor_texture": ${comp_armor_texture.value},\n`;
        }
        if (comp_decal_model.value != "" && comp_decal_texture.value != "") {
            json += `  "comp_decal_model": ${comp_decal_model.value},\n`;
            json += `  "comp_decal_texture": ${comp_decal_texture.value},\n`;
        }
        if (comp_top_model.value != "" && comp_shoes_texture.value != "") {
            json += `  "comp_top_model": ${comp_top_model.value},\n`;
            json += `  "comp_shoes_texture": ${comp_shoes_texture.value},\n`;
        }
        if (prop_hat_model.value != "" && prop_hat_texture.value != "") {
            json += `  "prop_hat_model": ${prop_hat_model.value},\n`;
            json += `  "prop_hat_texture": ${prop_hat_texture.value},\n`;
        }
        if (prop_glasses_model.value != "" && prop_glasses_texture.value != "") {
            json += `  "prop_glasses_model": ${prop_glasses_model.value},\n`;
            json += `  "prop_glasses_texture": ${prop_glasses_texture.value},\n`;
        }
        if (prop_ear_model.value != "" && prop_ear_texture.value != "") {
            json += `  "prop_ear_model": ${prop_ear_model.value},\n`;
            json += `  "prop_ear_texture": ${prop_ear_texture.value},\n`;
        }
        if (prop_watch_model.value != "" && prop_watch_texture.value != "") {
            json += `  "prop_watch_model": ${prop_watch_model.value},\n`;
            json += `  "prop_watch_texture": ${prop_watch_texture.value},\n`;
        }
        json = json.slice(0, json.length - 2);
        json += `\n}`;

        output.innerText = json;
    }
}