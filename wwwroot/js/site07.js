// NOW I CLICK album-poster TO GET CURRENT SONG ID
$(".album-poster").on('click', function (e) {
	var dataSwitchId = $(this).attr('data-switch');
	//console.log(dataSwitchId);

	// and now i use aplayer switch function see
	ap.list.switch(dataSwitchId); //this is static id but i use dynamic 

	// aplayer play function
	// when i click any song to play
	ap.play();

	// click to slideUp player see
	$("#aplayer").addClass('showPlayer');
});

const ap = new APlayer({
	container3: document.getElementById('aplayer'),
	listFolded: true,
	audio: [
		{
			name: 'null',
			artist: 'null',
			url: 'Music\sonreir.mp3',
			cover: 'FtCancion\sonreir.jfif',
		},
		{
			name: 'null',
			artist: 'null',
			url: 'null',
			cover: 'null'
		},
		{
			name: 'null',
			artist: 'null',
			url: 'null',
			cover: 'null'
		},
		//de aqui
		{
			name: 'Sonreír',
			artist: 'Kurt',
			url: 'Music\sonreir.mp3',
			cover: '\FtCancion\sonreir.jfif',
		},
		{
			name: 'La chispa adecuada	',
			artist: 'Héroes del silencio	',
			url: '\Music\La chispa adecuada.mp3',
			cover: '\FtCancion\La chispa adecuada.jfif',
		},
		{
			name: 'So Happy',
			artist: 'Theory of a Deadman	',
			url: '\Music\so happy.mp3',
			cover: '\FtCancion\so happy 2.jfif',
		},
		{
			name: 'Bad Girlfriend-theory',
			artist: 'Theory of a Deadman	',
			url: '\Music\bad girlfriend theory.mp3',
			cover: '\FtCancion\bad girlfriend theory.jfif',
		},
		{
			name: 'Sos',
			artist: 'Avicii',
			url: '\Music\sos.mp3',
			cover: '\FtCancion\Sos.jpg',
		},
		{
			name: 'null',
			artist: 'null',
			url: 'null',
			cover: 'null'
		},
		{
			name: 'null',
			artist: 'null',
			url: 'null',
			cover: 'null'
		},
		{
			name: 'Freak',
			artist: 'Avicii',
			url: '\Music\Freak.mp3',
			cover: '\FtCancion\Freak.jpg',
		},
		{
			name: 'Peace Of Mind',
			artist: 'Avicii',
			url: '\Music\Peaceofmind.mp3',
			cover: '\FtCancion\Peace of mind.jpg',
		},
		{
			name: 'Excuse Me Mr Sir',
			artist: 'Avicii',
			url: '\Music\Excuse Me Mr Sir.mp3',
			cover: '\FtCancion\Excuse Me Mr Sir.jpg',
		},
		{
			name: 'Intro',
			artist: 'Romeo Santos',
			url: '\Music\Intro.mp3',
			cover: '\FtCancion\fórmula vol 1.jpg',
		},
		{
			name: 'La Diabla',
			artist: 'Romeo Santos',
			url: '\Music\La Diabla.mp3',
			cover: '\FtCancion\fórmula vol 1.jpg',
		},
		{
			name: 'Procura',
			artist: 'Chichi Peralta',
			url: '\Music\Procura.mp3',
			cover: '\FtCancion\pao otro lao.jpg',
		},
		{
			name: 'Amor Narcótico',
			artist: 'Chichi Peralta',
			url: '\Music\Amor Narcótico.mp3',
			cover: '\FtCancion\pao otro lao.jpg',
		},
		{
			name: 'Vuelve',
			artist: 'Beret',
			url: '\Music\Vuelve.mp3',
			cover: '\FtCancion\Vuelve.jfif',
		},
		{
			name: 'Desde Cero',
			artist: 'Beret',
			url: '\Music\desde cero.mp3',
			cover: '\FtCancion\desde cero.jfif',
		},
		{
			name: 'La Chica de Humo',
			artist: 'Emmanuel',
			url: '\Music\La Chica de Humo.mp3',
			cover: '\FtCancion\La Chica de Humo.jfif',
		},
		{
			name: 'El Universo entre tus ojos',
			artist: 'David Rees',
			url: '\Music\El Universo entre tus ojos.mp3',
			cover: '\FtCancion\El Universo entre tus ojos.jfif',
		},
		{
			name: 'Luz de Día',
			artist: 'Enanitos Verdes',
			url: '\Music\Luz de Día.mp3',
			cover: '\FtCancion\Huevos revueltos.jfif',
		},
		{
			name: 'El Ataque de las Chicas cocodrilo',
			artist: 'Enanitos Verdes',
			url: '\Music\El Ataque de las Chicas cocodrilo.mp3',
			cover: '\FtCancion\Huevos revueltos.jfif',
		},
		{
			name: 'Mariposa Traicionera',
			artist: 'Maná',
			url: '\Music\Mariposa Traicionera.mp3',
			cover: '\FtCancion\Mariposa Traicionera.jfif',
		},
		{
			name: 'Eres mi Religión',
			artist: 'Maná',
			url: '\Music\Eres mi Religión.mp3',
			cover: '\FtCancion\Eres mi Religión.jfif',
		},
		{
			name: 'La Flaca',
			artist: 'Jarabe de Palo',
			url: '\Music\La Flaca.mp3',
			cover: '\FtCancion\La Flaca1.jfif',
		},
		{
			name: 'De Música Ligera',
			artist: 'Soda Stereo',
			url: '\Music\De Música Ligera.mp3',
			cover: '\FtCancion\De Música Ligera.jfif',
		},
		{
			name: 'Loco por volverte a ver',
			artist: 'Chili Fernández',
			url: '\Music\Loco por volverte a ver.mp3',
			cover: '\FtCancion\cancion.jpg',
		},


	]
});