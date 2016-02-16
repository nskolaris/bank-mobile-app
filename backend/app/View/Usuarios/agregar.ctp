<script>
	function Cancelar(){
		window.location = "<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'index')); ?>";
	}
</script>
<?php 
echo $this->Form->create('Usuario');?><div class="form-body"><?php
	echo '<div class="form-group">'.$this->Form->input('Usuario.nombre',array('value'=>(isset($usuario)?$usuario['Usuario']['nombre']:''),'class'=>'form-control')).'</div>';
	echo '<div class="form-group">'.$this->Form->input('Usuario.apellido',array('value'=>(isset($usuario)?$usuario['Usuario']['apellido']:''),'class'=>'form-control')).'</div>';
	echo '<div class="form-group">'.$this->Form->input('Usuario.username',array('value'=>(isset($usuario)?$usuario['Usuario']['username']:''),'class'=>'form-control')).'</div>';
	echo '<div class="form-group">'.$this->Form->input('Usuario.password_tablet',array('label'=>'Contraseña que será usada en la aplicación'.($this->action != 'agregarPromotora'?'(Debe ser distinta a la contraseña de la web)':''),'value'=>(isset($usuario)?$usuario['Usuario']['password_tablet']:''),'class'=>'form-control')).'</div>';
	if(isset($usuario)){
		echo $this->Form->input('Usuario.id',array('type'=>'hidden','value'=>$usuario['Usuario']['id']));
	}
	if($this->action != 'agregarPromotora'){
	echo '<div class="form-group">'.$this->Form->input('Usuario.password',array('label'=>'Contraseña Web','class'=>'form-control','required'=>(isset($usuario)?false:true))).'</div>';}
	echo '<div class="form-footer"><div class="pull-right"><button class="btn btn-danger" type="button" onClick="Cancelar();">Cancelar</button><button class="btn btn-success" type="submit">Guardar</button></div><div class="clearfix"></div></div>';
echo $this->Form->end();
?>