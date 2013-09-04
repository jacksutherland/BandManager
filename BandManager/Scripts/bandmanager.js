var BandManagerUI = function () {

    this.loadSortables = function (listSelector, handleSelector, postbackUrl) {
        $(listSelector).sortable({
            handle: handleSelector,
            //containment: "parent",
            stop: function () {
                if (typeof (postbackUrl) == "string") {
                    var serialized = "";
                    $.each($(listSelector).sortable("toArray"), function (index, value) {
                        serialized += (value + ",");
                    });
                    $.ajax({
                        url: postbackUrl,
                        data: { serialized: serialized },
                        type: "post",
                        cache: false,
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert("Error encountered while reordering. Unable to save order.");
                        }
                    });
                }
            }
        });
    };

};