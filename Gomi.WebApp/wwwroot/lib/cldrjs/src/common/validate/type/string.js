define([
	"../type"
], function( validateType ) {

	return function( value, name ) {
		validateType( value, name, typeof value === "string", "a string" );
	};

});
