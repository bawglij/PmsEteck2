$(function () {
    var ajaxFormSubmit = function (event) {

        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-pmseteck-target"));
            $target.replaceWith(data);
            $form.find(':submit').removeClass("hasBeenClicked");
        });

        return false;
    };

    var getPage = function () {
        var $a = $(this);
        if (typeof $a.attr("href") !== typeof undefined) {
            var options = {
                url: $a.attr("href"),
                data: $("form").serialize(),
                type: "get"
            };

            $.ajax(options).done(function (data) {
                var target = $a.parents("div.indexlist").attr("data-pmseteck-target");
                $(target).replaceWith(data);
            });
        }
        return false;
    };

    var getModalPage = function () {
        var $a = $(this);
        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            $('#workOrderList').replaceWith(data);
        });
    };

    var goToPage = function (event) {
        var $tr = $(this);
        if (typeof $tr.data("pmseteck-target") !== typeof undefined) {
            location.href = $tr.data("pmseteck-target");
        }
        return false;
    }

    var goToAction = function (event) {

        var $button = $(this);

        var options = {
            url: $button.attr("href"),
            type: 'GET'
        };

        $.ajax(options).done(function (data) {
            var $target = $($button.attr("data-pmseteck-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    $("form[data-pmseteck-ajax='true']").submit(ajaxFormSubmit);

    $("#body-content").on("click", ".pagedList a", getPage);

    $("#body-content").on("click", ".tableheader a", getPage);

    $("#body-content").on("click", ".ajaxmodal a", getModalPage);

    $("#body-content").on("click", "tr[data-pmseteck-clickable='true']", goToPage);

    $("#body-content").on("click", "a[data-ajax='true']", goToAction);

});

function removeNestedForm(element, container, deleteElement) {
    $container = $(element).parents(container);
    $container.find(deleteElement).val('True');
    $container.hide();
}

function addNestedForm(container, counter, ticks, content) {
    var nextIndex = $(counter).length;
    var pattern = new RegExp(ticks, "gi");
    content = content.replace(pattern, nextIndex);
    $(container).append(content);
}

function isAlpha(input) {
    return /[a-zA-z]/.test(input);
}

function isAlphaNumeric(input) {
    return /[a-zA-z0-9]/.test(input);
}

function isValidIBAN(input) {
    if (input != null && input != '') {
        $.ajax({
            url: 'https://openiban.com/validate/' + input,
            data: {
                "validateBankCode": true,
                "getBIC": true
            },
            success: function (data) {
                return data;
            },
            error: function (xhr) {
                return false;
            }
        });
    }
}

function updateActiveItem(event) {
    $('.projectYearDetailsLink').removeClass('active');
    $(event).addClass('active');
}