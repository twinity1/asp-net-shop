$(function () {
    function getCookie(cname) {
        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for(let i = 0; i <ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
    
   $(".shopping-cart").on("click", ".mad-close-item", async function () {
       const id = $(this).attr("data-id");
       
       const request = await fetch(`/Cart/Delete/${id}`, {
           method: "DELETE",
           headers: {
               Accept: "application/json",
           }
       });
       
       if (request.status !== 200) {
           alert("Produkt se nepodařilo z košíku odstranit.");
       }
   });
});