<script>
	function Cancelar(){
		window.location = "<?php echo $this->Html->Url(array('controller'=>'eventos','action'=>'index')); ?>";
	}
	
	$(document).ready(function(){
		$('#EventoFechaDesde').datepicker({dateFormat: "yyyy-mm-dd"});
		$('#EventoFechaHasta').datepicker({dateFormat: "yyyy-mm-dd"});
	});
</script>
<?php 
echo $this->Form->create('Evento');?><div class="form-body"><?php
	echo '<div class="form-group">'.$this->Form->input('Evento.code',array('label'=>'Código único','value'=>(isset($evento)?$evento['Evento']['code']:''),'class'=>'form-control')).'</div>';
	echo '<div class="form-group">'.$this->Form->input('Evento.nombre',array('value'=>(isset($evento)?$evento['Evento']['nombre']:''),'class'=>'form-control')).'</div>';
	echo '<div class="form-group">'.$this->Form->input('Evento.ciudad',array('value'=>(isset($evento)?$evento['Evento']['ciudad']:''),'class'=>'form-control')).'</div>';

	echo '<div class="form-group">'.$this->Form->input('Evento.fecha_desde',array('class'=>'form-control','type'=>'text','data-date-format'=>'yyyy-mm-dd','value'=>(isset($evento)?$evento['Evento']['fecha_desde']:''))).'</div>';
	
	echo '<div class="form-group">'.$this->Form->input('Evento.fecha_hasta',array('class'=>'form-control','type'=>'text','data-date-format'=>'yyyy-mm-dd','value'=>(isset($evento)?$evento['Evento']['fecha_hasta']:''))).'</div>';
	
	if(isset($evento)){
		echo $this->Form->input('Evento.id',array('type'=>'hidden','value'=>$evento['Evento']['id']));
	}
	echo '<div class="form-footer"><div class="pull-right"><button class="btn btn-danger" type="button" onClick="Cancelar();">Cancelar</button><button class="btn btn-success" type="submit">Guardar</button></div><div class="clearfix"></div></div>';
echo $this->Form->end();
?>