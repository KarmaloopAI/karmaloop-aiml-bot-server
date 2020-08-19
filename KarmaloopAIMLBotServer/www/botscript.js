var ele = document.createElement("div");
ele.setAttribute("class", "out");
ele.style.width = "70px";
ele.style.height = "70px";
ele.style.borderRadius = "50%";
ele.style.position = "absolute";
ele.style.bottom = "0";

var icon = document.createElement("div");
icon.innerHTML = '<i  class="fa fa-comments fa-2x"></i>';
icon.setAttribute("id", "iconID");
icon.setAttribute("onClick", "click()");
ele.appendChild(icon);
icon.style.textAlign = "center";
icon.style.paddingTop = "20px";

var element = document.createElement("div");
element.innerHTML = ' <iframe src ="{{externalBaseUrl}}/api/ChatUi/index.html" height="500" width="300" ></iframe>';
element.setAttribute("id", "iframeholder");
element.setAttribute("class", "frameclass");
element.style.right = '0';
element.style.bottom = '0';
element.style.display = 'none';

document.body.appendChild(ele);
document.body.appendChild(element);

var icon_tracker = 'c';
icon.onclick = function () {
    var ic = document.getElementById('iconID');
    if (icon_tracker == 'c') {
        ic.style.animation = 'rotateOut 0.3s';
        element.style.animation = 'zoomIn 0.5s';
        element.style.display = 'unset';
        ic.innerHTML = '<i class="fa fa-angle-double-down  fa-2x" ></i>';
        icon_tracker = 'f';
       
     
    } else {
        icon.style.animation = 'rotateIn 0.3s';
        element.style.animation = 'zoomOutRight 1s';
        setTimeout(function () {
            element.style.display = 'none';

        }, 1000);
       
        icon.innerHTML = '<i  class="fa fa-comments fa-2x"></i>';
        icon_tracker = 'c';
      }
    

}





