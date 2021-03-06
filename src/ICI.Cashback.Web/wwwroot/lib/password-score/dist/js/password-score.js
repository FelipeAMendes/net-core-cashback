(function ($) {
	var settings;
	$.fn.passwordStrong = function (options) {
		settings = $.extend({
			ratings: ["Muito Fraca", "Fraca", "Regular", "Forte", "Muito Forte"],
			progressClasses: ['progress-bar-danger', 'progress-bar-warning', 'progress-bar-info', 'progress-bar-success', 'progress-bar-success']
		}, options);
		var $passwordInput = $(settings.passwordInput),
				$progress = this;
		if (!settings.passwordInput) throw new TypeError('Please enter password input');
		$passwordInput.on('keyup', function () {
			updateProgress($passwordInput, $progress);
		});
		updateProgress($passwordInput, $progress);
	};
	function updateProgress($passwordInput, $progress) {
		var passwordValue = $passwordInput.val();
		if (passwordValue) {
			var result = zxcvbn(passwordValue, settings.userInputs),
					score = result.score,
					scorePercentage = (score + 1) * 20;
			$progress.css('width', scorePercentage + '%');
			$progress.removeClass(settings.progressClasses.join(' ')).addClass(settings.progressClasses[score]).text(settings.ratings[score]);
		} else {
			$progress.css('width', 0 + '%');
			$progress.removeClass(settings.progressClasses.join(' ')).text('');
		}
	}
})(jQuery);