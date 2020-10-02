/*
	jQuery Plugin
	Name: Collapsible Progress Bar
	Version: 0.9
	Description: Uses Twitter Bootstrap 3.0 styling to present an accessible, simple progress bar for a web form, with customizable messages of encouragement.
	Dependencies: Bootstrap 3.0 (http://getbootstrap.com/), Font Awesome 4.0.3 (http://fortawesome.github.io/Font-Awesome/), jQuery 1.10.2 or later (http://jquery.com/)
	Author: Joshua Blackwood
	Copyright: 2013-2014 Joshua Blackwood under the MIT License (http://opensource.org/licenses/MIT)
 */

(function ($) {

    $.fn.showProgress = function (options) {

        var defaults = {
            message: {
                '25': 'You\'re doing great so far!',
                '50': 'You\'re halfway there!',
                '75': 'Look at that, you\'re nearly done already!',
                '100': 'All done! Just click <strong>Submit</strong> to continue!'
            },
            color: {
                '25': '#DB4437',
                '50': '#4285F4',
                '75': '#F4B400',
                '100': '#0F9D58'
            },
            position: 'bottom',
            valuenow: $("#ProgressValue").val(),
            width: $("#ProgressValue").val()
        };

        options = $.extend(defaults, options);

        var markup = '<!-- Progress Bar --><div class="navbar navbar-default navbar-fixed-' + options.position + '" id="progress-bar-wrap" style="display:block"><div class="container"><h4 id="percent-complete"> ' + options.valuenow +'% Complete<small class="encouragement"></small><button type="button" class="close hidden" data-dismiss="progress-bar" aria-hidden="true" title="Collapse" style="display:none"><span class="sr-only">Collapse</span></button></h4><div class="progress"><div id="form-progress" class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="' + options.valuenow + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + options.width + '%"><span class="sr-only">0% Complete</span></div></div></div></div> <!-- /progress bar -->';

        $('#markupContainer').append(markup);

        var dismiss = $('button[data-dismiss="progress-bar"]'),
            radioSiblings = $(this).find('input[type=radio][required].required, input.required').parents('label').siblings().children('input[type=radio]'),
            input = $(this).find('textarea.progress-required, input.progress-required, select.progress-required').add(radioSiblings),
            magicNumber = 100 / (input.length - radioSiblings.length),
            pbw = $('#progress-bar-wrap');

        dismiss.on('click', function () {
            pbw.toggleClass('collapsed', 300);
        });

        input.data('progress-percent', '0');

        input.change(function () {

           
            var $this = $(this),

                progressBar = $('#form-progress'),
                srText = $('#form-progress > span'),
                avn = progressBar.attr('aria-valuenow'),
                hasProgress = $this.hasClass('progress-percent'),
                siblingInput = $this.parents('label').siblings().children('input'),
                siblingName = siblingInput.attr('name'),
                encouragement = $('.encouragement');

            if (pbw.not(':visible')) {
                pbw.show(300);
            }
          
             var _continue = false;
            if (hasProgress == false && $.trim($this.val())) {
                if ($this.attr('name') == siblingName) {
                    siblingInput.data('progress-percent', '1');
                }
                avnMath = parseFloat(avn) + parseFloat(magicNumber);
                updateAVN = avnMath.toFixed(2);
                pbWidth = updateAVN;
                if (pbWidth > 100) { pbWidth = 100; updateAVN = 100; }
                if (pbWidth < 0) { pbWidth = 0; updateAVN = 0;}
                progressBar.width(pbWidth + '%');
                $("#ProgressValue").val(pbWidth);
                progressBar.attr('aria-valuenow', updateAVN);
                srText.text(updateAVN + '% Complete');
                $("#percent-complete").text(updateAVN + '% Complete');
                $this.addClass('progress-percent');
                _continue = true;
                console.log('AVN is: ' + updateAVN);
            } else if (hasProgress == true && !$.trim($this.val())) { //If the field value is emptied, we need to remove that progress.
                $this.removeClass('progress-percent');
                avnMath = parseFloat(avn) - parseFloat(magicNumber);
                updateAVN = avnMath.toFixed(2);
                pbWidth = updateAVN;
                if (pbWidth > 100) { pbWidth = 100; updateAVN = 100; }
                if (pbWidth < 0) { pbWidth = 0; updateAVN = 0; }
                $("#ProgressValue").val(pbWidth);
                progressBar.width(pbWidth + '%');
                progressBar.attr('aria-valuenow', updateAVN);
                srText.text(updateAVN + '% Complete');
                $("#percent-complete").text(updateAVN + '% Complete');
                _continue = true;
                console.log('AVN is: ' + updateAVN);
            }
            if (_continue) {
                for (var key in options.message) {
                    keyMatch = parseFloat(key - 5.00);
                    var value = options.message[key];
                    var color = options.color[key];
                    if (encouragement.not(':visible')) {
                        encouragement.show(300);
                    }
                    if (parseFloat(updateAVN) >= parseFloat(keyMatch)) {
                        encouragement.html(value);
                        $(".progress-bar").css("background-color", color);
                        console.log(key + ' : ' + value);
                    }
                }
            }

        });


    };

   // $("#progress-bar-wrap").css("display", "block");

})(jQuery);

