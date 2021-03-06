$(function () {
	login.configure();
});

var Login = (function () {
	function Login() { };

	Login.prototype.configure = function () {
		addConfigs();
	}

	function addConfigs() {
		$("#Password").on("cut copy paste", function (event) {
			event.preventDefault();
		});

		$("#Cpf").mask("000.000.000-00", { placeholder: "___.___.___-__" });
	}

	return Login;
}());

var login = new Login();