var body = document.getElementsByTagName("body")[0];
var lolData = [];
// var lol = document.getElementById("lol");
function Getdata() {
    $.ajax({
        type: "GET",
        url: "https://raw.githubusercontent.com/jacky637006/jacky637006/master/HW/lol_js.json",
        dataType: "json",
        success: function (response) {
            lolData = response;
            for (let i = 0; i < lolData.length; i++) {
                Importdata(i, lolData[i].icon, lolData[i].id, lolData[i].id, lolData[i].stats.hp,lolData[i].stats.attackdamage,lolData[i].stats.mp,lolData[i].stats.mpperlevel
                ,lolData[i].stats.movespeed,lolData[i].stats.attackrange);

            }
        }
    });
}




function Importdata(number, img_src, nametxt, smallnametxt, hptxt, attacktxt, mptxt,mpperleveltxt,movespeedtxt,attackrangetxt) {
    let template_lol = document.getElementById("template_lol");
    let cloneContent = template_lol.content.cloneNode(true);
    let bigbtn = cloneContent.getElementById("hero_item");
    let btnalert = cloneContent.querySelector(".btnalert");
    var idnumber_btn = `#exampleModalLong${number}`;
    var idnumber_aleart = `exampleModalLong${number}`;
    bigbtn.setAttribute("data-target", idnumber_btn);
    btnalert.setAttribute("id", idnumber_aleart);
    let heroimg = cloneContent.getElementById("heroimg");
    let name = cloneContent.getElementById("namethemetxt");
    let smallnametxtgr = cloneContent.getElementById("aleart_itemname");
    let modalbodytxt = cloneContent.getElementById("modal-bodytxt");
    heroimg.setAttribute("src", img_src);
    name.textContent = nametxt;
    smallnametxtgr.textContent = smallnametxt;
    let smallcontent = `血量:${hptxt}\n${attacktxt}\n魔力:${mptxt}每級提升${mpperleveltxt}點mp\n移動速度${movespeedtxt}\n攻擊範圍:${attackrangetxt}`;
    modalbodytxt.textContent = smallcontent;
    body.appendChild(cloneContent);
}




Getdata();