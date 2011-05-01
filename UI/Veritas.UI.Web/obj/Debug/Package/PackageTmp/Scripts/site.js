//Fired when the js is first launched
$(document).ready(function () {

    // override Highslide settings here
    // instead of editing the highslide.js file
    hs.graphicsDir = '/Scripts/highslide/graphics/';
    hs.align = 'center';
    hs.transitions = ['expand', 'crossfade'];
    hs.outlineType = 'rounded-white';
    hs.wrapperClassName = 'controls-in-heading';
    hs.fadeInOut = true;
    //hs.dimmingOpacity = 0.75;

    // Add the controlbar
    if (hs.addSlideshow) hs.addSlideshow({
        //slideshowGroup: 'group1',
        interval: 5000,
        repeat: false,
        useControls: true,
        fixedControls: false,
        overlayOptions: {
            opacity: 1,
            position: 'top right',
            hideOnMouseOut: false
        }
    });

    //Match the heights for our inner tube
    //var tallest = $('.innertube').equalHeight();
    //Set the left menu bar to tallest
    //$('.leftMenuBar').css({ 'height': tallest  });
    FixColumns();
});

function FixColumns() {
    //Match the heights for our inner tube
    var tallest = $('.innertube').equalHeight();
    //Set the left menu bar to tallest
    $('.leftMenuBar').css({ 'height': tallest });
};

// Equal Height
$.fn.equalHeight = function () {
    var tallest = 0;
    $(this).each(function () {
        if (tallest < $(this).height()) {
            //            tallest = $(this).height() / .832;
            tallest = $(this).height();
        }
    });
    $(this).css({ 'height': tallest });
    return tallest;
};

function ShowHideComments(id) {
    $('#divComments' + id).slideToggle(true);
    setTimeout('FixColumns()', 1000);
}