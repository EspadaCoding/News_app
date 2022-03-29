 
//document.addEventListener("DOMContentLoaded", function () {
  
//});

function HideShowDiv(id) {
    var x = document.getElementById("CommentSee-"+id);
    if (x.style.display === "none") {
        x.style.display = "block";
        document.getElementById("Com-" + id).innerHTML = "Close comments";
        document.getElementById("countC-" + id).style.display = "none";  
    } else {
       
        x.style.display = "none";
        document.getElementById("Com-" + id).innerHTML = "View comments";
        document.getElementById("countC-" + id).style.display = "block"; 
    }
}


//try to make comments with ajax request 
//function Clickcomment(id) {
//    var link = "/News/CommentThis/"+id;
//    var content = $('#InputContent-'+id).val(); 
//    $('#InputContent-'+id).val("");  
//  if (content != " ") {
//      $.ajax({
//          type: "POST", 
//          url: link, 
//          data: {
//              id, content
//          },
//          success: function (data) {
//              $('#InputContent-' + id).html(data);
//          }

//      }); 
//    }
//    else {
//        alert("Please write comment");
//    }
//}