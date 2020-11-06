var chat_icon =  `<svg width="40" height="40" x="0" y="0" viewBox="0 0 373.232 373.232" style="enable-background:new 0 0 512 512" xml:space="preserve">
<g>
 <g>
	<g>
		<path d="M187.466,0c-0.1,0-0.3,0-0.6,0c-101.2,0-183.5,82.3-183.5,183.5c0,41.3,14.1,81.4,39.9,113.7l-26.7,62.1    c-2.2,5.1,0.2,11,5.2,13.1c1.8,0.8,3.8,1,5.7,0.7l97.9-17.2c19.6,7.1,40.2,10.7,61,10.6c101.2,0,183.5-82.3,183.5-183.5    C370.066,82.1,288.366,0.1,187.466,0z M124.666,146.6h54c5.5,0,10,4.5,10,10s-4.5,10-10,10h-54c-5.5,0-10-4.5-10-10    S119.166,146.6,124.666,146.6z M248.666,216.6h-124c-5.5,0-10-4.5-10-10s4.5-10,10-10h124c5.5,0,10,4.5,10,10    S254.166,216.6,248.666,216.6z" fill="#ffffff" data-original="#000000" style=""/>
	</g>
 </g>
 </g>
</svg>
`;

var down_icon = `
<svg  width="40" height="40" x="0" y="0" viewBox="0 0 496.096 496.096" style="enable-background:new 0 0 512 512" xml:space="preserve"><g>
<g>
 <g>
   <path d="M259.41,247.998L493.754,13.654c3.123-3.124,3.123-8.188,0-11.312c-3.124-3.123-8.188-3.123-11.312,0L248.098,236.686    L13.754,2.342C10.576-0.727,5.512-0.639,2.442,2.539c-2.994,3.1-2.994,8.015,0,11.115l234.344,234.344L2.442,482.342    c-3.178,3.07-3.266,8.134-0.196,11.312s8.134,3.266,11.312,0.196c0.067-0.064,0.132-0.13,0.196-0.196L248.098,259.31    l234.344,234.344c3.178,3.07,8.242,2.982,11.312-0.196c2.995-3.1,2.995-8.016,0-11.116L259.41,247.998z" fill="#ffffff" data-original="#000000" style=""/>
 </g>
</g>
</g>
</svg>
`;


var ele = document.createElement("div");
ele.setAttribute("class", "out");
ele.style.width = "80px";
ele.style.height = "80px";
ele.style.borderRadius = "50%";
ele.style.position = "sticky";
ele.style.bottom = "20px";
ele.style.left = "90%";
ele.style.right= "0";
ele.style.marginRight = "20px";
ele.style.marginBottom = "20px";
ele.style.backgroundColor = "rgb(0,191,255)";
ele.style.boxShadow = " 0 0 20px 1.5px rgb(0,191,255)";


var icon = document.createElement("div");
icon.setAttribute("id", "iconID");
icon.setAttribute("onClick", "click()");
icon.innerHTML = chat_icon;
icon.style.position = "relative";
icon.style.left = "20px";
icon.style.top = "20px";
ele.appendChild(icon);


var element = document.createElement("div");
element.innerHTML = ' <iframe src ="{{externalBaseUrl}}/api/ChatUi/index.html" height="500" width="300" ></iframe>';
element.setAttribute("id", "iframeholder");
element.setAttribute("class", "frameclass");
element.style.position = "fixed";
element.style.right = '0';
element.style.bottom = '15%';
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
        icon_tracker = 'f';
  
        icon.innerHTML = down_icon;
    } else {
        icon.style.animation = 'rotateIn 0.3s';
        element.style.animation = 'zoomOutRight 1s';
        setTimeout(function () {
            element.style.display = 'none';

        }, 1000);
 
        icon.innerHTML = chat_icon;
        icon_tracker = 'c';
      }
}