﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//Made By: David Manshari! at 4am :(

// Write your JavaScript code.


$(function ()
{
 
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Show More Products Button CODE
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Show Reviews AREA CODE
    //vars to keep reviews we loaded 5 reviews
    var skipCount = 0;
    var takeCount = 6;
    var hasMoreRecords = true;
    var name = null;
    var brand = 0;
    var category = 0;
    var sort = "newest";
    showProducts();



    function showProducts() {
        $.ajax({
            url: '/Products/getProducts',
            data: { productName: name, brandId: brand, categoryId: category, sort: sort, skipCount: skipCount, takeCount: takeCount },
            success: function (data) {
                if (data == 0) {
                    hasMoreRecords = false; // signal no more records to display
                    $('#ShowMoreProducts').text("No More Products To Show!").css('background-color', 'red');
                }
                else {
                    $('#resultsProdcuts').tmpl(data).appendTo('#productInsertion').hide().fadeIn(1000);
                    skipCount += takeCount; // update for next iteration
                    $("#showMoreButton").show(); //button after turning red and reload get hide() so need to show him again after reload
                }
            },
            error: function () {
                alert("error");
            }
        });
    }
    

    //show more products button
    $(document).on("click", '#ShowMoreProducts', function (event) {
        $([document.documentElement, document.body]).animate({
            scrollTop: $("#resultsProdcuts").offset().top
        } ,400);
        showProducts();
    });




    $(".select").on('change', function () {  //the dropbox select
        name = null;
        brand = $("#BrandId").val();
        category = $("#CategoryId").val();
        sort = $("#Sort").val();
        skipCount = 0;
        takeCount = 6;
        hasMoreRecords = true;
        $("#showMoreButton").hide(); //remove button because might be red and jumpy
        $(".col-xl-4").remove(); //remove other products
        showProducts();
        $("#showMoreButton").load(window.location.href + " #showMoreButton");
        
       

    });


});







