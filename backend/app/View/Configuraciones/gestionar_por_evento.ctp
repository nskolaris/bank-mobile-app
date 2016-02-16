<script>
	function Cancelar(){
		window.location = "<?php echo $this->Html->Url(array('controller'=>'eventos','action'=>'index')); ?>";
	}
</script>
<div class="pull-left">
	<h3 class="panel-title">Configuración para el evento <?php echo $evento['Evento']['nombre']; ?></h3>
</div><br><br>

<?php echo $this->Form->create('Configuracion'); ?><div class="form-body">

	<?php echo $this->Form->input('Configuracion.evento_id',array('type'=>'hidden','value'=>$evento['Evento']['id'])); ?>

	<!--Juegos-->
	
	<div class="form-group">
	<?php echo $this->Form->input('juego_activo',array(
		'label' 	=> 'Juego activo?',
		'type'		=> 'checkbox',
		'checked'	=> (isset($configuraciones['juego_activo'])?($configuraciones['juego_activo']==1?true:false):true),
		'class'		=> 'form-control'
	)); ?>
	</div>
	
	<div class="form-group">
	<?php echo $this->Form->input('juego_nombre',array(
		'label' 	=> 'Nombre del juego activo',
		'options'	=> array('memotest'=>'Memotest','trivia'=>'Trivia','jumper'=>'Recorriendo el País'),
		'value'		=> (isset($configuraciones['juego_nombre'])?$configuraciones['juego_nombre']:''),
		'class'		=> 'form-control'
	)); ?>
	</div>
	
	<p id="settings_juegos">
	
		<!--Memotest-->
	
		<div class="form-group">
		<?php echo $this->Form->input('memotest_tiempo',array(
			'label' => 'Tiempo limite de memotest',
			'options'	=> array('30'=>'30','45'=>'45','60'=>'60'),
			'value'		=> (isset($configuraciones['memotest_tiempo'])?$configuraciones['memotest_tiempo']:''),
			'class'		=> 'form-control'
		)); ?>
		</div>
		
		<div class="form-group">
		<?php echo $this->Form->input('memotest_grupo_id',array(
			'label' 	=> 'Grupo activo memotest',
			'options'	=> array('1'=>'Entretenimientos','2'=>'Productos','3'=>'Sustentabilidad'),
			'value'		=> (isset($configuraciones['memotest_grupo_id'])?$configuraciones['memotest_grupo_id']:''),
			'class'		=> 'form-control'
		)); ?>
		</div>
		
		<!--Trivia-->
		
		<div class="form-group">
		<?php echo $this->Form->input('trivia_grupo_id',array(
			'label' 	=> 'Grupo activo trivia',
			'options'	=> array('1'=>'Promociones','2'=>'Eventos','3'=>'Satisfacción','4'=>'Institucional'),
			'value'		=> (isset($configuraciones['trivia_grupo_id'])?$configuraciones['trivia_grupo_id']:''),
			'class'		=> 'form-control'
		)); ?>
		</div>
		
	</p>
	
	<!--Premios-->
	
	<div class="form-group">
	<?php echo $this->Form->input('premios_activos',array(
		'label' => 'Ruleta activa?',
		'type'	=> 'checkbox',
		'checked'	=> (isset($configuraciones['premios_activos'])?($configuraciones['premios_activos']==1?true:false):true),
		'class'		=> 'form-control'
	)); ?>
	</div>
	
	<p id="premios_juegos">
	
		<!--Premios settings-->
	
		<div class="form-group">
		<?php echo $this->Form->input('cantidad_premios',array(
			'label' => 'Cantidad de premios',
			'value'	=> (isset($configuraciones['cantidad_premios'])?$configuraciones['cantidad_premios']:''),
			'class'		=> 'form-control'
		)); ?>
		</div>
		
	</p>
	
	<!--Formulario-->
	
	<div class="form-group">
	<?php echo $this->Form->input('formulario_activo',array(
		'label' => 'Formulario activo?',
		'type'	=> 'checkbox',
		'checked'	=> (isset($configuraciones['formulario_activo'])?($configuraciones['formulario_activo']==1?true:false):true),
		'class'		=> 'form-control'
	)); ?>
	</div>
	
	
	<!--Header-->
	
	<div class="form-group">
	<?php echo $this->Form->input('header_banco_id',array(
		'label' 	=> 'Encabezado activo',
		'options'	=> array('1'=>'Banco Macro','2'=>'Banco Tucuman'),
		'value'		=> (isset($configuraciones['header_banco_id'])?$configuraciones['header_banco_id']:''),
		'class'		=> 'form-control'
	)); ?>
	</div>

</div>
<div class="form-footer">
	<div class="pull-right">
		<button class="btn btn-danger" type="button" onClick="Cancelar();">Cancelar</button>
		<button class="btn btn-success" type="submit">Guardar</button>
	</div>
	<div class="clearfix"></div>
</div><?php echo $this->Form->end(); ?>